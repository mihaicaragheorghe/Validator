namespace SimpleValidator;

public class ValidationException : Exception
{
    public ValidationException(ValidationResult result) 
        : this(result.Errors) { }

    public ValidationException(IEnumerable<ValidationError> errors)
        : base("One or more validation errors occured:" + Environment.NewLine + string.Join(Environment.NewLine, errors.Select(e => e.ToString())))
    {
        Errors = errors;
    }

    public IEnumerable<ValidationError> Errors { get; }
}