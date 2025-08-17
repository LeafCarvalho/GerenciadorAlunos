namespace GerenciadorAlunos.DTOs;

public class AlunoCreateRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required string Password { get; set; }
}

public class AlunoResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
}

public class AlunoListItem
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
