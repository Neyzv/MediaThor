using System.Collections.Immutable;

namespace MediaThor.SourceGenerator.RequestHandler;

public sealed record PipelineBehaviorInformation(string PipelineName, ImmutableArray<string> InterfacePipelineName, uint Priority);