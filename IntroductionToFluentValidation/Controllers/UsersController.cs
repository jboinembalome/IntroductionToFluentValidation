using FluentValidation;
using IntroductionToFluentValidation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace IntroductionToFluentValidation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IValidator<CreateUserRequest> validator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        // Validate the request using the injected validator asynchronously
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // Check if validation fails
        if (!validationResult.IsValid)
        {
            // Copy the errors into the ModelState, so that ValidationProblem() formats them
            // the same way as ASP.NET Core's own validation errors (type, title, status, traceId)
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            // Return a 400 Bad Request response with a ValidationProblemDetails body
            return ValidationProblem(ModelState);
        }

        // Process the valid request...
        return Ok();
    }
}
