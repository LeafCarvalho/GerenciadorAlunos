using GerenciadorAlunos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorAlunos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos => Set<Aluno>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
