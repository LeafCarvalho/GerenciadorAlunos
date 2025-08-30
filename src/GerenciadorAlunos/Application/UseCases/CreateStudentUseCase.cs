using GerenciadorAlunos.Domain.Contracts;
using GerenciadorAlunos.Domain.Services;

namespace GerenciadorAlunos.Application.UseCases;

public sealed class CreateStudentUseCase
{
    private readonly StudentService _service;
    private readonly IStudentRepository _repo;

    public CreateStudentUseCase(StudentService service, IStudentRepository repo)
    {
        _service = service;
        _repo = repo;
    }

    public async Task<ValidationResult<CreateStudentOutput>> ExecuteAsync(
        CreateStudentInput input,
        CancellationToken ct = default)
    {
        var valid = _service.Validate(input);
        if (!valid.IsValid)
            return ValidationResult<CreateStudentOutput>.Falha(valid.Errors);

        var email = valid.Value!.Email;
        var passwordHash = valid.Value.PasswordHash;

        if (await _repo.EmailExistsAsync(email, ct))
            return ValidationResult<CreateStudentOutput>.Falha("Email já cadastrado.");

        var id = await _repo.SaveAsync(
            name: (input.Name ?? string.Empty).Trim(),
            email: email,
            phone: input.Phone,
            passwordHash: passwordHash,
            ct: ct
        );

        return ValidationResult<CreateStudentOutput>.Ok(new CreateStudentOutput(id, email));
    }
}
