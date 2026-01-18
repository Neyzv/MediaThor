<p align="center">
  <img src="icon.png" alt="MediaThor" width="160" />
  <h1 align="center">MediaThor</h1>
</p>

<p align="center">
    Simplify communications between components in your C# applications.<br>
    <b>MediaThor</b> provides a source-generated, CQRS-friendly Mediator pattern with built-in support for dependency injection.
</p>

---

## Features
- Mediator CQRS-friendly
- Source generated request dispatcher
- Source generated request handling pipeline
- Request handling pipeline priority configuration


## Getting Started
Install the package via NuGet :
```bash
dotnet add package MediaThor
```
Or using the Package Manager :
```bash
Install-Package MediaThor
```


## Usage
MediaThor works just like others. To get started start by adding the generated services to your DI container with the `AddMediaThor` extension method :
```cs
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddMediaThor();  // <-- here

var app = builder.Build();
```
\
Create your feature, here for the exemple we will keep it as light as possible :
```cs
public sealed record SayHelloQuery(string Name) : IRequest<string>;

public sealed class SayHelloHandler
    : IRequestHandler<SayHelloQuery, string>
{
    public Task<string> HandleAsync(SayHelloQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult($"Hello {request.Name} !");
    }
}
```
\
Then create your endpoint and inject the IMediaThor instance : \
*Minimal API*
```cs
app.MapGet("/hello/{name}", async (
    string name,
    IMediaThor mediator,  // <-- here
    CancellationToken cancellationToken) =>
{
    var query = new SayHelloQuery(name);
    var result = await mediator.Send(query, cancellationToken);

    return Results.Ok(result);
});
```
*Controller Based API*
```cs
[ApiController]
[Route("[controller]")]
public class HelloController(IMediaThor mediator)  // <-- here
    : ControllerBase
{
    // GET api/hello/{name}
    [HttpGet("{name}")]
    public async Task<IActionResult> SayHello(string name, CancellationToken cancellationToken)
    {
        var query = new SayHelloQuery(name);
        var result = await mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}
```
\
And you're done !

## Going further
### IPipelineBehavior
Just like some others library allows you can add some pipeline behaviors. \
In this exemple we will implement two dummy behaviors. All behaviors should implement the `IPipelineBehavior<TRequest, TResponse>` interface. \
\
We will start by creating a generic one, which will log in debug the received request.
```cs
public sealed class DebugLogBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Debug.WriteLine($"Received request : '{request}'.")

        return await next(cancellationToken);
    }
}
```
\
And another one more specific which will be executed only if the request is a `SayHelloQuery`.
```cs
public sealed class HelloSaidBehavior
    : IPipelineBehavior<SayHelloQuery, string>
{
    public async Task<string> HandleAsync(SayHelloQuery request, RequestHandlerDelegate<string> next, CancellationToken cancellationToken)
    {
        if (request.Name.Contains(' '))
            throw new InvalidOperationException();

        return await next(cancellationToken);
    }
}
```
\
You can also define a priority order on your `IPipelineBehavior` just by decorating the class with the `MediaThorPipePriorityAttribute`.
```cs
[MediaThorPipePriority(0)]  // <-- This pipeline behavior will be executed before the others. 
public sealed class HelloSaidBehavior
    : IPipelineBehavior<SayHelloQuery, string>
...
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.