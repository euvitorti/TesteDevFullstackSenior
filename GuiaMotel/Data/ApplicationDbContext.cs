using GuiaMotel.Model;
using Microsoft.EntityFrameworkCore;

namespace GuiaMotel.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Defina os DbSets correspondentes às suas entidades. Por exemplo:
        public DbSet<User> Users { get; set; }

        // Caso tenha outras entidades, adicione-as aqui:
        // public DbSet<OutraEntidade> OutrasEntidades { get; set; }
    }
}
