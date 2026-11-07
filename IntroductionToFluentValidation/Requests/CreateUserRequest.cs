namespace IntroductionToFluentValidation.Requests;

public sealed record CreateUserRequest(string FirstName, string LastName, string Email);
