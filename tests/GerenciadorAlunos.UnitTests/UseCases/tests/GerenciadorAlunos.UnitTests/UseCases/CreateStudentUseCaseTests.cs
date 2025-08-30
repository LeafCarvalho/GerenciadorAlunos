using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GerenciadorAlunos.Application.UseCases;
using GerenciadorAlunos.Domain.Contracts;
using GerenciadorAlunos.Domain.Services;
using GerenciadorAlunos.UnitTests._Fakes;
using Xunit;

namespace GerenciadorAlunos.UnitTests.UseCases;

public sealed class CreateStudentUseCaseTests
{
    private sealed class StudentRepositoryStub : IStudentRepository
    {
        public bool EmailJaExiste { get; set; }
        public (string name, string email, string? phone, string passwordHash)? UltimoSalvo { get; private set; }
        public Guid IdParaRetornar { get; set; } = Guid.NewGuid();

        public Task<bool> EmailExistsAsync(string email, CancellationToken ct)
            => Task.FromResult(EmailJaExiste);

        public Task<Guid> SaveAsync(string name, string email, string? phone, string passwordHash, CancellationToken ct)
        {
            UltimoSalvo = (name, email, phone, passwordHash);
            return Task.FromResult(IdParaRetornar);
        }

        public Task<IReadOnlyList<(Guid Id, string Name, string Email)>> GetAllAsync(CancellationToken ct)
        => Task.FromResult<IReadOnlyList<(Guid, string, string)>>(Array.Empty<(Guid, string, string)>());

        public Task<(Guid Id, string Name, string Email, string? Phone)?> GetByIdAsync(Guid id, CancellationToken ct)
            => Task.FromResult<(Guid, string, string, string?)?>(null);
    }

    private static CreateStudentInput Input(string name, string email, string? phone, string password)
      => new CreateStudentInput(name, email, phone, password);


    [Fact]
    public async Task Deve_falhar_quando_email_ja_existe()
    {
        var repo = new StudentRepositoryStub { EmailJaExiste = true };
        var svc  = new StudentService(new FakePasswordHasher());
        var use  = new CreateStudentUseCase(svc, repo);

        var input = Input("Ana", "ana@mail.com", null, "Secr3t!");

        var res = await use.ExecuteAsync(input, TestContext.Current.CancellationToken);

        res.IsValid.Should().BeFalse();
        res.Errors.Should().Contain("Email já cadastrado.");
    }

    [Fact]
    public async Task Deve_salvar_quando_validacao_ok_e_email_unico()
    {
        var repo = new StudentRepositoryStub { EmailJaExiste = false, IdParaRetornar = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") };
        var svc  = new StudentService(new FakePasswordHasher());
        var use  = new CreateStudentUseCase(svc, repo);

        var input = Input("Ana", "ANA@mail.com", null, "Secr3t!");

        var res = await use.ExecuteAsync(input, TestContext.Current.CancellationToken);

        res.IsValid.Should().BeTrue();
        res.Value!.Id.Should().Be(repo.IdParaRetornar);
        res.Value.Email.Should().Be("ana@mail.com");
        repo.UltimoSalvo!.Value.passwordHash.Should().StartWith("HASH(");
        repo.UltimoSalvo!.Value.name.Should().Be("Ana");
    }
}
