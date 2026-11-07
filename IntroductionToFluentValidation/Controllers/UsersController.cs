using FluentValidation;
using IntroductionToFluentValidation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace IntroductionToFluentValidation.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController(IValidator<CreateUserRequest> validator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    public async Task<IActionResult> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            // ValidationProblem() then formats the errors like ASP.NET Core's own validation errors.
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

        // Process the valid request...
        return Ok();
    }
}
