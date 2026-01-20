using Microsoft.CodeAnalysis;

namespace MediaThor.SourceGenerator.RequestHandler;

[Generator(LanguageNames.CSharp)]
public sealed partial class RequestHandlerSourceGenerator
    : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var streamRequestHandlers =
            context.SyntaxProvider
                .CreateSyntaxProvider(PredicateStreamHandlers, TransformHandlers)
                .Collect();
        
        var requestHandlers = context.SyntaxProvider
            .CreateSyntaxProvider(PredicateHandlers, TransformHandlers)
            .Collect()
            .Combine(streamRequestHandlers);
        
        var services = context.SyntaxProvider
            .CreateSyntaxProvider(PredicatePipelines, TransformPipelines)
            .Collect()
            .Combine(requestHandlers);

        context.RegisterSourceOutput(requestHandlers, static (spc, source) => GenerateMediaThorDispatcher(spc, source.Left, source.Right));
        context.RegisterSourceOutput(services, static (spc, source) => GenerateMediaThorServiceCollectionExtensions(spc, source.Left, source.Right.Left,  source.Right.Right));
    }
}