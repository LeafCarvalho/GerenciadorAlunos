namespace GerenciadorAlunos.DTOs;

public class AlunoCreateRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Telefone { get; set; }
    public required string Senha { get; set; }
}

public class AlunoResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Telefone { get; set; }
}

public class AlunoListItem
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
