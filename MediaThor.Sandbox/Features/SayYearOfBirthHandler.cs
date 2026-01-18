namespace MediaThor.Sandbox.Features;

public sealed record SayYearOfBirthQuery(byte Age) : IRequest<ushort>;

public sealed class SayYearOfBirthHandler
    : IRequestHandler<SayYearOfBirthQuery, ushort>
{
    public Task<ushort> HandleAsync(SayYearOfBirthQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult((ushort)(DateTime.Now.Year - request.Age));
    }
}