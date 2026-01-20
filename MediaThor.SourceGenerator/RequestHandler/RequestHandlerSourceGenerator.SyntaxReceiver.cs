using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediaThor.SourceGenerator.RequestHandler;

public sealed partial class RequestHandlerSourceGenerator
{
    private const string RequestHandlerClassName = "IRequestHandler<";
    private const string StreamRequestHandlerClassName = "IStreamRequestHandler<";
    private const string PipelineClassName = "IPipelineBehavior<";
    private const string MediaThorPipePriorityAttributeName = "MediaThorPipePriorityAttribute";
    
    private static bool PredicateHandlers(SyntaxNode node, CancellationToken ct)
    {
        if (node is not ClassDeclarationSyntax classSyntax)
            return false;

        if (classSyntax.BaseList is null)
            return false;
        
        if (classSyntax.TypeParameterList is not null)
            return false;

        return classSyntax
            .BaseList
            .Types
            .Select(static x => x.Type)
            .Any(static x => x.ToString().Contains(RequestHandlerClassName));
    }

    private static RequestHandlerInformation TransformHandlers(GeneratorSyntaxContext ctx, CancellationToken ct)
    {
        if (ctx.SemanticModel.GetDeclaredSymbol(ctx.Node) is not INamedTypeSymbol symbol)
            throw new Exception("Can not get declared symbol.");

        var handledTypes = symbol
            .AllInterfaces
            .Select(x => x.ToDisplayString())
            .Where(x => x.Contains(RequestHandlerClassName) || x.Contains(StreamRequestHandlerClassName))
            .ToImmutableArray();

        if (handledTypes.IsEmpty)
            throw new Exception("Can not get empty handled types list.");

        return new RequestHandlerInformation(symbol.ToDisplayString(), handledTypes);
    }
    
    private static bool PredicateStreamHandlers(SyntaxNode node, CancellationToken ct)
    {
        if (node is not ClassDeclarationSyntax classSyntax)
            return false;

        if (classSyntax.BaseList is null)
            return false;
        
        if (classSyntax.TypeParameterList is not null)
            return false;

        return classSyntax
            .BaseList
            .Types
            .Select(static x => x.Type)
            .Any(static x => x.ToString().Contains(StreamRequestHandlerClassName));
    }
    
    private static bool PredicatePipelines(SyntaxNode node, CancellationToken ct)
    {
        if (node is not ClassDeclarationSyntax classSyntax)
            return false;

        if (classSyntax.BaseList is null)
            return false;

        return classSyntax
            .BaseList
            .Types
            .Select(static x => x.Type)
            .Any(x => (classSyntax.TypeParameterList is null || (x is GenericNameSyntax g && classSyntax.TypeParameterList?.Parameters.Count == g.TypeArgumentList.Arguments.Count)) && x.ToString().Contains(PipelineClassName));
    }

    private static PipelineBehaviorInformation TransformPipelines(GeneratorSyntaxContext ctx, CancellationToken ct)
    {
        if (ctx.SemanticModel.GetDeclaredSymbol(ctx.Node) is not INamedTypeSymbol symbol)
            throw new Exception("Can not get declared symbol.");
        
        var pipelineBehaviors = symbol
            .AllInterfaces
            .Select(x => x.Arity is 0 || x.Arity != symbol.Arity ? x.ToDisplayString() : x.ToDisplayString().Split('<')[0] + '<' + new string(',', x.Arity - 1) + '>')
            .Where(x => x.Contains(PipelineClassName))
            .ToImmutableArray();

        if (pipelineBehaviors.IsEmpty)
            throw new Exception("Can not get empty pipeline behavior list.");

        var priority = (uint)(symbol
            .GetAttributes()
            .FirstOrDefault(static x => x.AttributeClass?.Name == MediaThorPipePriorityAttributeName)?.ConstructorArguments
            .First().Value ?? uint.MaxValue);
        
        return new PipelineBehaviorInformation(symbol.Arity is 0 ? symbol.ToDisplayString() : symbol.ToDisplayString().Split('<')[0] + '<' + new string(',', symbol.Arity - 1) + '>', pipelineBehaviors, priority);
    }
}