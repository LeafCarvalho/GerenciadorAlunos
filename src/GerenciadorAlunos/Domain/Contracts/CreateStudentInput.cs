namespace GerenciadorAlunos.Domain.Contracts;

public sealed record CreateStudentInput
{
    public required string Name  { get; init; }
    public required string Email { get; init; }
    public string? Phone         { get; init; }
    public required string Password { get; init; }
}