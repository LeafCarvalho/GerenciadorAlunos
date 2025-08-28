namespace GerenciadorAlunos.Domain.Contracts;

public interface IStudentRepository
{
  Task<bool> EmailExistsAsync(string email, CancellationToken ct);
  Task<Guid> SaveAsync(string name, string email, string? phone, string passwordHash, CancellationToken ct);
}
