namespace GerenciadorAlunos.Domain.Contracts;

public sealed class ValidationResult<T>
{
    public bool IsValid { get; init; }
    public List<string> Errors { get; } = new();
    public T? Value { get; init; }
}
