namespace SimpleValidator.Tests;

public class ValidationTests
{
    [Fact]
    public void Validator_WithValidUser_ShouldReturnValidResult()
    {
        // Arrange
        var user = TestUtils.CreateUser();

        // Act
        var result = new CreateUserCommandValidator(user).GetResult();

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validator_WithInvalidUser_ShouldReturnCustomError()
    {
        // Arrange
        var user = TestUtils.CreateUser(username: string.Empty);

        // Act
        var result = new CreateUserCommandValidator(user).GetResult();

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any());
        Assert.Contains(result.Errors, e => e.Code == "User.UsernameRequired");
        Assert.Contains(result.Errors, e => e.Message == "Username is required");
    }

    [Fact]
    public void Validator_WithInvalidUser_ShouldReturnCustomErrorMessage()
    {
        // Arrange
        var user = TestUtils.CreateUser(age: 17);

        // Act
        var result = new CreateUserCommandValidator(user).GetResult();

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any());
        Assert.Contains(result.Errors, e => e.Code == "User.Invalid");
        Assert.Contains(result.Errors, e => e.Message == "Age must be greater than or equal to 18");
    }
    
    [Fact]
    public void Validator_WithInvalidUser_ShouldReturnDefaultError()
    {
        // Arrange
        var user = TestUtils.CreateUser(username: "a");

        // Act
        var result = new CreateUserCommandValidator(user).GetResult();

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any());
        Assert.Contains(result.Errors, e => e.Code == "User.Invalid");
        Assert.Contains(result.Errors, e => e.Message == "User is invalid");
    }

    [Fact]
    public void Validator_WithMultipleValidationErrors_ShouldReturnAllErrors()
    {
        // Arrange
        var user = TestUtils.CreateUser(username: string.Empty, age: 17);

        // Act
        var result = new CreateUserCommandValidator(user).GetResult();

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any());
        Assert.Contains(result.Errors, e => e.Code == "User.UsernameRequired");
        Assert.Contains(result.Errors, e => e.Message == "Username is required");
        Assert.Contains(result.Errors, e => e.Code == "User.Invalid");
        Assert.Contains(result.Errors, e => e.Message == "Age must be greater than or equal to 18");
    }

    [Fact]
    public void Validator_WithInvalidUser_ShouldThrowValidationException()
    {
        // Arrange
        var user = TestUtils.CreateUser(username: string.Empty);

        // Act
        var exception = Assert.Throws<ValidationException>(() => new CreateUserCommandValidator(user).ValidateAndThrow());

        // Assert
        Assert.NotNull(exception);
        Assert.NotEmpty(exception.Errors);
        Assert.Contains("User.UsernameRequired", exception.Message);
        Assert.Contains("Username is required", exception.Message);
        Assert.Contains(exception.Errors, e => e.Code == "User.UsernameRequired");
        Assert.Contains(exception.Errors, e => e.Message == "Username is required");
    }

    [Fact]
    public void Validator_WithInvalidUser_ShouldThrowValidationExceptionWithAllErrors()
    {
        // Arrange
        var user = TestUtils.CreateUser(username: string.Empty, age: 17);

        // Act
        var exception = Assert.Throws<ValidationException>(() => new CreateUserCommandValidator(user).ValidateAndThrow());

        // Assert
        Assert.NotNull(exception);
        Assert.NotEmpty(exception.Errors);
        Assert.Contains("User.UsernameRequired", exception.Message);
        Assert.Contains("Username is required", exception.Message);
        Assert.Contains("User.Invalid", exception.Message);
        Assert.Contains("Age must be greater than or equal to 18", exception.Message);
        Assert.Contains(exception.Errors, e => e.Code == "User.UsernameRequired");
        Assert.Contains(exception.Errors, e => e.Message == "Username is required");
        Assert.Contains(exception.Errors, e => e.Code == "User.Invalid");
        Assert.Contains(exception.Errors, e => e.Message == "Age must be greater than or equal to 18");
    }

    [Fact]
    public void Validator_WithInvalidUser_ShouldNotAddSameErrorTwice()
    {
        // Arrange
        var user = TestUtils.CreateUser(username: "x", email: string.Empty);

        // Act
        var result = new CreateUserCommandValidator(user).GetResult();

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any());
        Assert.Single(result.Errors);
    }
}