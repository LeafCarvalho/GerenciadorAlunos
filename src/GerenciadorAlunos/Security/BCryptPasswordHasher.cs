using GerenciadorAlunos.Domain.Contracts;

namespace GerenciadorAlunos.Security;

public sealed class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string plainText) => BCrypt.Net.BCrypt.HashPassword(plainText);
    public bool Verify(string hash, string plainText) => BCrypt.Net.BCrypt.Verify(plainText, hash);
}