using Xunit;
using FluentAssertions;

namespace GerenciadorAlunos.UnitTests;

public class AlunoCriacaoTestes
{
    [Fact]
    public void CreateStudent_returns_error_when_name_is_missing()
    {
        var svc = new StudentService(new FakePasswordHasher());
        var input = new CreateStudentInput(name: "", email: "ana@mail.com", phone: null, password: "Secr3t!");

        var result = svc.Validate(input);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain("O campo nome é de preenchimento obrigatório.");
    }

    [Fact]
    public void CreateStudent_returns_error_when_email_is_missing()
    {
        var svc = new StudentService(new FakePasswordHasher());
        var input = new CreateStudentInput(name: "Ana", email: "", phone: null, password: "Secr3t!");

        var result = svc.Validate(input);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain("O campo email é de preenchimento obrigatório.");
    }

    [Fact]
    public void CreateStudent_returns_error_when_email_is_invalid()
    {
        var svc = new StudentService(new FakePasswordHasher());
        var input = new CreateStudentInput("Ana", "email-invalido", null, "Secr3t!");

        var result = svc.Validate(input);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain("Email em um formato inválido.");
    }

    [Fact]
    public void CreateStudent_allows_verification_of_password_via_hasher()
    {
        var hasher = new FakePasswordHasher();
        var svc = new StudentService(hasher);
        var input = new CreateStudentInput("Ana", "ana@mail.com", null, "Secr3t!");

        var result = svc.Validate(input);

        result.IsValid.Should().BeTrue();

        var ok = hasher.Verify(result.Value.PasswordHash, "Secr3t!");
        ok.Should().BeTrue();
    }
}

public class StudentCreationTests
{
    [Fact]
    public void CreateStudent_returns_error_when_password_is_missing()
    {
        var svc = new StudentService(new FakePasswordHasher());
        var input = new CreateStudentInput("Ana", "ana@mail.com", null, "");

        var result = svc.Validate(input);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain("O campo senha é de preenchimento obrigatório.");
    }

    [Fact]
    public void CreateStudent_hashes_password_and_does_not_leak_plain_password()
    {
        var svc = new StudentService(new FakePasswordHasher());
        var input = new CreateStudentInput("Ana", "ANA@mail.com", null, "Secr3t!");

        var result = svc.Validate(input);

        result.IsValid.Should().BeTrue();
        result.Value.Email.Should().Be("ana@mail.com");
        result.Value.PasswordHash.Should().NotBeNullOrWhiteSpace();
        result.Value.PasswordHash.Should().NotBe("Secr3t!");
        result.Value.PasswordHash.Should().StartWith("HASH(");
    }

    [Fact]
    public void CreateStudent_allows_verification_of_password_via_hasher()
    {
        var hasher = new FakePasswordHasher();
        var svc = new StudentService(hasher);
        var input = new CreateStudentInput("Ana", "ana@mail.com", null, "Secr3t!");

        var result = svc.Validate(input);

        result.IsValid.Should().BeTrue();
        hasher.Verify(result.Value.PasswordHash, "Secr3t!").Should().BeTrue();
    }
}
