using FluentValidation;
using MediaThor;
using MediaThor.Sandbox.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi()
    .AddMediaThor()
    .AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.MapGet("/hello/{name}", async (
    string name,
    IMediaThor mediator,
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
        IMediaThor mediator,
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

