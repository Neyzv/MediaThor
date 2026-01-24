using Microsoft.CodeAnalysis;

namespace MediaThor.SourceGenerator.RequestHandler;

[Generator(LanguageNames.CSharp)]
public sealed partial class RequestHandlerSourceGenerator
    : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var requestHandlers = context.SyntaxProvider
            .CreateSyntaxProvider(PredicateHandlers, TransformHandlers)
            .Collect();
        
        var streamRequestHandlers =
            context.SyntaxProvider
                .CreateSyntaxProvider(PredicateStreamHandlers, TransformHandlers)
                .Collect();
        
        var genericRequestHandlers = context.SyntaxProvider
            .CreateSyntaxProvider(PredicateGenericHandlers, TransformHandlers)
            .Collect()
            .Combine(streamRequestHandlers)
            .Combine(requestHandlers);
        
        var services = context.SyntaxProvider
            .CreateSyntaxProvider(PredicatePipelines, TransformPipelines)
            .Collect()
            .Combine(genericRequestHandlers);

        context.RegisterSourceOutput(genericRequestHandlers, static (spc, source) => GenerateMediaThorDispatcher(spc, source.Left.Left, source.Left.Right));
        context.RegisterSourceOutput(services, static (spc, source) => GenerateMediaThorServiceCollectionExtensions(spc, source.Left, source.Right.Left.Left,  source.Right.Left.Right, source.Right.Right));
    }
}