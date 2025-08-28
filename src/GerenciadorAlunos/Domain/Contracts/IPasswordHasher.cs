namespace GerenciadorAlunos.Domain.Contracts;

public interface IPasswordHasher
{
    string Hash(string plainText);
    bool Verify(string hash, string plainText);
}
