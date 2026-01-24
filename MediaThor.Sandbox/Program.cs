using FluentValidation;
using MediaThor;
using MediaThor.Sandbox.Features;
using MediaThor.Sandbox.Services.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi()
    .AddMediaThor()
    .AddSingleton<IRequestValidationService, RequestValidationService>()
    .AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.MapGet("/hello/{name}", async (
    string name,
    IMediator mediator,
    CancellationToken cancellationToken) =>
{
    var query = new SayHelloQuery(name);
    var result = await mediator.Send(query, cancellationToken);

    return Results.Ok(result);
})
.WithName("SayHello")
.Produces<string>(StatusCodes.Status200OK)
.WithSummary("Say hello.")
.WithDescription("Simple route to say hello.");

app.MapGet("/year/{age:int}", async (
        byte age,
        IMediator mediator,
        CancellationToken cancellationToken) =>
    {
        var query = new SayYearOfBirthQuery(age);
        var result = await mediator.Send(query, cancellationToken);

        return Results.Ok(result);
    })
    .WithName("SayYearOfBirth")
    .Produces<ushort>(StatusCodes.Status200OK)
    .WithSummary("Say year of birth.")
    .WithDescription("Simple route to say the year of birth.");

app.MapGet("/enum/{amount:int}", (
        byte amount,
        IMediator mediator,
        CancellationToken cancellationToken) =>
    {
        var query = new StreamedQuery(amount);

        return mediator.CreateStream(query, cancellationToken);
    })
    .WithName("EnumerateValues")
    .Produces<ushort>(StatusCodes.Status200OK)
    .WithSummary("Enumerate random int values.")
    .WithDescription("Simple route to stream int values.");

app.MapGet("/nocontent", async (
        IMediator mediator,
        CancellationToken cancellationToken) =>
    {
        var query = new NoContentQuery();
        await mediator.Send(query, cancellationToken);

        return Results.NoContent();
    })
    .WithName("NoContent")
    .Produces<ushort>(StatusCodes.Status204NoContent)
    .WithSummary("No content here.")
    .WithDescription("Simple route to test no content.");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

