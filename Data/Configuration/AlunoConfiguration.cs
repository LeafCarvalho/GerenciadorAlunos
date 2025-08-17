using GerenciadorAlunos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorAlunos.Data;

public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
  public void Configure(EntityTypeBuilder<Aluno> builder)
  {
    builder.ToTable("Alunos");

    builder.HasKey(a => a.Id);

    builder.Property(a => a.Name)
      .IsRequired()
      .HasMaxLength(120);

    builder.Property(a => a.Email)
      .IsRequired()
      .HasMaxLength(254);

    builder.HasIndex(a => a.Email)
      .IsUnique();

    builder.Property(a => a.Phone)
      .HasMaxLength(20);

    builder.Property(a => a.PasswordHash)
      .IsRequired()
      .HasMaxLength(255);
  }
}
