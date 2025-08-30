using GerenciadorAlunos.Application.UseCases;
using GerenciadorAlunos.Domain.Contracts;
using GerenciadorAlunos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorAlunos.Controllers;

[ApiController]
[Route("alunos")]
public sealed class AlunosController : ControllerBase
{
    private readonly CreateStudentUseCase _useCase;
    private readonly IStudentRepository _repo;

    public AlunosController(CreateStudentUseCase useCase, IStudentRepository repo)
    {
        _useCase = useCase;
        _repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AlunoCreateRequest request, CancellationToken ct)
    {
        var input = new CreateStudentInput(request.Name, request.Email, request.Phone, request.Password);
        var result = await _useCase.ExecuteAsync(input, ct);

        if (!result.IsValid)
            return BadRequest(new { errors = result.Errors });

        var response = new AlunoResponse
        {
            Id = result.Value!.Id,
            Name = request.Name,
            Email = result.Value.Email,
            Phone = request.Phone
        };

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var alunos = await _repo.GetAllAsync(ct);

        var response = alunos.Select(a => new AlunoListItem
        {
            Id = a.Id,
            Name = a.Name,
            Email = a.Email
        });

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var aluno = await _repo.GetByIdAsync(id, ct);
        if (aluno is null)
            return NotFound();

        var response = new AlunoResponse
        {
            Id = aluno.Value.Id,
            Name = aluno.Value.Name,
            Email = aluno.Value.Email,
            Phone = aluno.Value.Phone
        };

        return Ok(response);
    }
}
