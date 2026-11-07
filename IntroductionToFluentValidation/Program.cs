using FluentValidation;
using IntroductionToFluentValidation.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// FluentValidation is the only validation in this sample: do not let MVC reject a missing
// non-nullable property with its own message before the validator runs.
builder.Services.AddControllers(options =>
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();
app.MapUserEndpoints();

app.Run();
