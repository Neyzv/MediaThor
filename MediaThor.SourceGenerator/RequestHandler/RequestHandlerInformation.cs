using System.Collections.Immutable;

namespace MediaThor.SourceGenerator.RequestHandler;

public sealed record RequestHandlerInformation(string HandlerTypeName, ImmutableArray<string> HandledTypes);