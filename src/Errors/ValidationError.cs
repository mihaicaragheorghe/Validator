namespace SimpleValidator;

public class ValidationError
{
    public ValidationError(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }
    public string Message { get; }

    public override string ToString() => $"{Code}: {Message}";

    public override int GetHashCode() => HashCode.Combine(Code, Message);

    public override bool Equals(object? obj) => 
        obj is ValidationError error && error.Code == Code && error.Message == Message;
}