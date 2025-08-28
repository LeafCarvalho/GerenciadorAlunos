using GerenciadorAlunos.Domain.Contracts;
using GerenciadorAlunos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorAlunos.Data.Repositories;

public sealed class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _db;

    public StudentRepository(AppDbContext db) => _db = db;

    public Task<bool> EmailExistsAsync(string email, CancellationToken ct)
        => _db.Alunos
               .AsNoTracking()
               .AnyAsync(a => a.Email == email, ct);

    public async Task<Guid> SaveAsync(string name, string email, string? phone, string passwordHash, CancellationToken ct)
    {
        var entity = new Aluno
        {
            Name = name,
            Email = email,
            Phone = phone,
            PasswordHash = passwordHash,
        };

        _db.Alunos.Add(entity);
        await _db.SaveChangesAsync(ct);

        return entity.Id;
    }
}
