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

    public async Task<IReadOnlyList<(Guid Id, string Name, string Email)>> GetAllAsync(CancellationToken ct)
    {
        return await _db.Alunos
            .AsNoTracking()
            .Select(a => new ValueTuple<Guid, string, string>(a.Id, a.Name, a.Email))
            .ToListAsync(ct);
    }

    public async Task<(Guid Id, string Name, string Email, string? Phone)?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _db.Alunos
            .AsNoTracking()
            .Where(a => a.Id == id)
            .Select(a => new ValueTuple<Guid, string, string, string?>(a.Id, a.Name, a.Email, a.Phone))
            .FirstOrDefaultAsync(ct);
    }
}
