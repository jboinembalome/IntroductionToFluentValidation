# Introduction to FluentValidation

Source code used in the article [Validating requests with FluentValidation](https://blouppy.com/blog/2023-06-03-validating-requests-with-fluentvalidation/) on the [Blouppy](https://blouppy.com) website.

It validates a `CreateUserRequest` with a FluentValidation validator, in two flavours:

- `POST /api/users`: an MVC controller that injects `IValidator<CreateUserRequest>` and returns `ValidationProblem(...)`.
- `POST /api/minimal/users`: a Minimal API endpoint validated by a reusable endpoint filter, `ValidationFilter<T>`, that returns `Results.ValidationProblem(...)`.

Both answer an invalid request with a `400 Bad Request` and an `application/problem+json` body.

## Project type
- ASP.NET Core Web API (controllers and Minimal APIs)

## Project version
- .NET 10 (C# 14)

## NuGet packages
- **FluentValidation**: 12.2.0
- **FluentValidation.DependencyInjectionExtensions**: 12.2.0 (`AddValidatorsFromAssembly`)
- **Microsoft.AspNetCore.OpenApi**: 10.0.14

`FluentValidation.AspNetCore` is not used: it is deprecated on NuGet, and the sample validates explicitly instead of relying on its automatic validation.

## Run it

```bash
dotnet run --project IntroductionToFluentValidation --launch-profile http

curl -i -X POST http://localhost:5098/api/users \
  -H 'Content-Type: application/json' \
  -d '{"firstName":"Jimmy","lastName":"","email":"jimmy@example.com"}'

curl -i -X POST http://localhost:5098/api/minimal/users \
  -H 'Content-Type: application/json' \
  -d '{"firstName":"Jimmy","lastName":"","email":"not-an-email"}'
```

## Give a Star! :star:
If you like or are using this project please give it a star. Thanks!

## Support this project! :pray:
If you want to help me to maintain this project or add improvements, it would be a pleasure!
