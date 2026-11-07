using IntroductionToFluentValidation.Filters;
using IntroductionToFluentValidation.Requests;

namespace IntroductionToFluentValidation.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/minimal/users", (CreateUserRequest request) =>
            {
                // Process the valid request...
                return TypedResults.Ok();
            })
            .AddEndpointFilter<ValidationFilter<CreateUserRequest>>()
            .ProducesValidationProblem();

        return app;
    }
}
