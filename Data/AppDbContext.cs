using Microsoft.EntityFrameworkCore;

namespace GerenciadorAlunos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

    
    }
}
