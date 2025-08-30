namespace GerenciadorAlunos.Domain.Contracts;

public interface IStudentRepository
{
  Task<bool> EmailExistsAsync(string email, CancellationToken ct);
  Task<Guid> SaveAsync(string name, string email, string? phone, string passwordHash, CancellationToken ct);

  Task<IReadOnlyList<(Guid Id, string Name, string Email)>> GetAllAsync(CancellationToken ct);
  Task<(Guid Id, string Name, string Email, string? Phone)?> GetByIdAsync(Guid id, CancellationToken ct);
}
