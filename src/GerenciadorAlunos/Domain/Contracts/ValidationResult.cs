public sealed class ValidationResult<T>
{
    public bool IsValid { get; }
    public IReadOnlyList<string> Errors { get; }
    public T? Value { get; }

    private ValidationResult(bool isValid, IReadOnlyList<string> errors, T? value)
    {
        IsValid = isValid;
        Errors  = errors;
        Value   = value;
    }

    public static ValidationResult<T> Ok(T value)
        => new(true, Array.Empty<string>(), value);

    public static ValidationResult<T> Falha(params string[] erros)
        => new(false, erros, default);

    public static ValidationResult<T> Falha(IEnumerable<string> erros)
        => new(false, erros.ToList(), default);
}
