namespace GerenciadorAlunos.Domain.Contracts;

public sealed class CreatedStudent
{
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }
}
