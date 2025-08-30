using System.Text.RegularExpressions;
using GerenciadorAlunos.Domain.Contracts;

namespace GerenciadorAlunos.Domain.Services;

public sealed class StudentService
{
    private readonly IPasswordHasher _hasher;

    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public StudentService(IPasswordHasher hasher) => _hasher = hasher;

    public ValidationResult<CreatedStudent> Validate(CreateStudentInput input)
    {
        var errors = new List<string>();

        var name     = (input.Name ?? string.Empty).Trim();
        var emailRaw = (input.Email ?? string.Empty).Trim();
        var password = (input.Password ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add("O campo nome é de preenchimento obrigatório.");

        if (string.IsNullOrWhiteSpace(emailRaw))
            errors.Add("O campo email é de preenchimento obrigatório.");
        else if (!EmailRegex.IsMatch(emailRaw))
            errors.Add("Email em um formato inválido.");

        if (string.IsNullOrWhiteSpace(password))
            errors.Add("O campo senha é de preenchimento obrigatório.");

        if (errors.Count > 0)
            return ValidationResult<CreatedStudent>.Falha(errors);

        var emailNormalized = emailRaw.ToLowerInvariant();
        var passwordHash    = _hasher.Hash(password);

        var created = new CreatedStudent
        {
            Email        = emailNormalized,
            PasswordHash = passwordHash
        };

        return ValidationResult<CreatedStudent>.Ok(created);
    }
}
