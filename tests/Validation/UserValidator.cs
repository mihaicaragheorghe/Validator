namespace SimpleValidator.Tests;

public class CreateUserCommandValidator : Validator<User>
{
    public CreateUserCommandValidator(User user) : base(user)
    {
        Validate(p => !string.IsNullOrWhiteSpace(p.Username),
            code: "User.UsernameRequired",
            message: "Username is required");

        Validate(p => p.Age >= 18, "Age must be greater than or equal to 18");

        Validate(p => p.Username.Length > 3);

        Validate(p => p.Email.Contains('@'));
    }
}