using FluentValidation;
using IntroductionToFluentValidation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace IntroductionToFluentValidation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IValidator<CreateUserRequest> _validator;
    
    public UsersController(IValidator<CreateUserRequest> validator)
    {
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        // Validate the request using the injected validator asynchronously
        var validationResult = await _validator.ValidateAsync(request);

        // Check if validation fails
        if (!validationResult.IsValid)
        {
            // Optional: Improve error response by formatting validation errors 
            // into a standardized format using the ValidationProblemDetails object
            var errors = validationResult.Errors
                .GroupBy(v => v.PropertyName, v => v.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

            var details = new ValidationProblemDetails(errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            // Return a 400 Bad Request response with the validation errors
            return BadRequest(new BadRequestObjectResult(details));
        }

        // Process the valid request...
        return Ok();
    }
}
