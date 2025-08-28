using GerenciadorAlunos.Domain.Contracts;

namespace GerenciadorAlunos.UnitTests._Fakes;

public sealed class FakePasswordHasher : IPasswordHasher
{
    public string Hash(string plainText) => $"HASH({plainText})";
    public bool Verify(string hash, string plainText) => hash == $"HASH({plainText})";
}
