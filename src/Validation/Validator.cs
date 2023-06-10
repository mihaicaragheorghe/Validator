namespace SimpleValidator;

public class Validator<T> where T : class
{

    private readonly ValidationResult _result = new();
    private readonly T _value;

    public Validator(T value)
    {
        _value = value;
    }

    public ValidationResult GetResult() => _result;

    public void ValidateAndThrow()
    {
        if (!_result.IsValid)
        {
            throw new ValidationException(_result);
        }
    }

    protected void Validate(Func<T, bool> predicate, string code, string message)
    {
        if (!predicate(_value))
        {
            var error = new ValidationError(code, message);
            if (!_result.Errors.Any(e => e.Equals(error)))
            {
                _result.Errors.Add(error);
            }
        }
    }

    protected void Validate(Func<T, bool> predicate, string message)
    {
        Validate(predicate, $"{typeof(T).Name}.Invalid", message);
    }

    protected void Validate(Func<T, bool> predicate)
    {
        Validate(predicate, $"{typeof(T).Name} is invalid");
    }
}