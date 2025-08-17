namespace GerenciadorAlunos.Models;

public class Aluno
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required string Email { get; set; }
  public string? Phone { get; set; }
  public required string PasswordHash { get; set; }
}