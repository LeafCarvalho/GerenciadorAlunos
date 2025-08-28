using Xunit;
using FluentAssertions;
using GerenciadorAlunos.Domain.Services;
using GerenciadorAlunos.UnitTests._Fakes;
using GerenciadorAlunos.Domain.Contracts;

namespace GerenciadorAlunos.UnitTests.Services;

public class StudentCreationTests
{
    private static StudentService NewSvc() => new(new FakePasswordHasher());
    private static CreateStudentInput Input(string name, string email, string? phone, string password)
        => new CreateStudentInput
        {
            Name = name,
            Email = email,
            Phone = phone,
            Password = password
        };


    [Fact]
    public void CreateStudent_returns_error_when_name_is_missing()
    {
        var svc = NewSvc();
        var input = Input("", "ana@mail.com", null, "Secr3t!");

        var result = svc.Validate(input);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain("O campo nome é de preenchimento obrigatório.");
    }

    [Fact]
    public void CreateStudent_returns_error_when_email_is_missing()
    {
        var svc = NewSvc();
        var input = Input("Ana", "", null, "Secr3t!");

        var result = svc.Validate(input);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain("O campo email é de preenchimento obrigatório.");
    }

    [Fact]
    public void CreateStudent_returns_error_when_email_is_invalid()
    {
        var svc = NewSvc();
        var input = Input("Ana", "email-invalido", null, "Secr3t!");

        var result = svc.Validate(input);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain("Email em um formato inválido.");
    }

    [Fact]
    public void CreateStudent_returns_error_when_password_is_missing()
    {
        var svc = NewSvc();
        var input = Input("Ana", "ana@mail.com", null, "");

        var result = svc.Validate(input);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain("O campo senha é de preenchimento obrigatório.");
    }

    [Fact]
    public void CreateStudent_hashes_password_and_does_not_leak_plain_password()
    {
        var svc = NewSvc();
        var input = Input("Ana", "ANA@mail.com", null, "Secr3t!");

        var result = svc.Validate(input);

        result.IsValid.Should().BeTrue();
        result.Value!.Email.Should().Be("ana@mail.com");
        result.Value.PasswordHash.Should().NotBeNullOrWhiteSpace();
        result.Value.PasswordHash.Should().NotBe("Secr3t!");
        result.Value.PasswordHash.Should().StartWith("HASH(");
    }

    [Fact]
    public void CreateStudent_allows_verification_of_password_via_hasher()
    {
        var hasher = new FakePasswordHasher();
        var svc = new StudentService(hasher);
        var input = Input("Ana", "ana@mail.com", null, "Secr3t!");

        var result = svc.Validate(input);

        result.IsValid.Should().BeTrue();
        hasher.Verify(result.Value!.PasswordHash, "Secr3t!").Should().BeTrue();
    }
}
