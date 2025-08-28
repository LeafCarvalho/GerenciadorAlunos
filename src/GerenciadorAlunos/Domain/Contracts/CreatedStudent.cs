namespace GerenciadorAlunos.Domain.Contracts;

public sealed record CreatedStudent(
    string Email,
    string PasswordHash
);
