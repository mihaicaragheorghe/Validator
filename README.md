# Validator
---
A very small .NET validation library

### Creating a validator
---
``` C#
public class CreateUserCommandValidator : Validator<User>
{
    public CreateUserCommandValidator(User user) : base(user)
    {
        Validate(u => u.Username.Length > 3);
    }
}
```

To get the result simply use the `GetResult()` method
``` C#
public void Create(User user)
{
    var validationResult = new CreateUserCommandValidator(user).GetResult();
}
```

This will return a `ValidationResult` object, which contains two properties, `IsValid` and `Errors`  
``` json
{
    "isValid": false,
    "errors: [
        {
            "code": "User.Invalid",
            "message": "User is invalid"
        }
    ]
}
```
These are the default error code and message. To override them, use the constructors  
Note: If the result already contains an error with the same code and name, it won't be added into the list
``` C#
// This will override both the error code and error message
Validate(u => !string.IsNullOrWhiteSpace(u.Username),
    code: "User.UsernameRequired",
    message: "Username is required");

// this will override only the error message
Validate(u => u.Age >= 18, "Age must be greater than or equal to 18");
```

### Throwing exceptions
---
To throw an exception, instead of the `GetResult()` method, you can call the `ValidateAndThrow()` method:
``` C#
public void Create(User user)
{
    var validator = new CreateUserCommandValidator(user);
    validator.ValidateAndThrow();
}
```
This will throw a `ValidationException` which contains the list of errors inside. The exception message is the concatenated errors, e.g.
``` 
One or more validation errors occured
User.UsernameRequired: Username is required
User.InvalidEmail: The provided e-mail address is in invalid format
```

### Schemas
``` 
Error:
    Code: string,
    Message: string
```

``` 
ValidationResult
    IsValid: bool,
    Errors: Error[]
```

``` 
ValidationException
    Errors: Error[]
```