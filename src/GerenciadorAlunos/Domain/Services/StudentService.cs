using System.Text.RegularExpressions;
using GerenciadorAlunos.Domain.Contracts;

namespace GerenciadorAlunos.Domain.Services;

public sealed partial class StudentService
{
    private readonly IPasswordHasher _hasher;

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase)]
    private static partial Regex EmailRegex();

    private const string MSG_NOME_OBRIGATORIO   = "O campo nome é de preenchimento obrigatório.";
    private const string MSG_EMAIL_OBRIGATORIO  = "O campo email é de preenchimento obrigatório.";
    private const string MSG_EMAIL_INVALIDO     = "Email em um formato inválido.";
    private const string MSG_SENHA_OBRIGATORIA  = "O campo senha é de preenchimento obrigatório.";

    public StudentService(IPasswordHasher hasher) => _hasher = hasher;

    public ValidationResult<CreatedStudent> Validate(CreateStudentInput input)
    {
        var errors   = new List<string>();
        var name     = Normalize(input.Name);
        var emailRaw = Normalize(input.Email);
        var password = Normalize(input.Password);

        ValidarNome(name, errors);
        ValidarEmail(emailRaw, errors);
        ValidarSenha(password, errors);

        if (errors.Count > 0)
            return ValidationResult<CreatedStudent>.Falha(errors);

        var emailNormalized = emailRaw.ToLowerInvariant();
        var passwordHash    = _hasher.Hash(password);

        var created = new CreatedStudent(emailNormalized, passwordHash);

        return ValidationResult<CreatedStudent>.Ok(created);
    }

    private static string Normalize(string? s) => (s ?? string.Empty).Trim();

    private static void ValidarNome(string name, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(name))
            errors.Add(MSG_NOME_OBRIGATORIO);
    }

    private static void ValidarEmail(string emailRaw, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(emailRaw))
        {
            errors.Add(MSG_EMAIL_OBRIGATORIO);
            return;
        }

        if (!EmailRegex().IsMatch(emailRaw))
            errors.Add(MSG_EMAIL_INVALIDO);
    }

    private static void ValidarSenha(string password, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(password))
            errors.Add(MSG_SENHA_OBRIGATORIA);
    }
}
