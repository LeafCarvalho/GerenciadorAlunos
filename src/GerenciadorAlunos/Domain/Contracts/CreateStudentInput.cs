namespace GerenciadorAlunos.Domain.Contracts;

public record CreateStudentInput(string Name, string Email, string? Phone, string Password);
