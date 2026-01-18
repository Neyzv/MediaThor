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
        
        var pipelines = context.SyntaxProvider
            .CreateSyntaxProvider(PredicatePipelines, TransformPipelines)
            .Collect()
            .Combine(requestHandlers);

        context.RegisterSourceOutput(requestHandlers, static (spc, source) => GenerateMediaThorDispatcher(spc, source));
        context.RegisterSourceOutput(pipelines, static (spc, source) => GenerateMediaThorServiceCollectionExtensions(spc, source.Left, source.Right));
    }
}