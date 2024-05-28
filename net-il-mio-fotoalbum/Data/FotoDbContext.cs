using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Data
{
    public class FotoDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Foto> Foto { get; set; }
        public DbSet<Categorie>? Categorie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Foto;" +
            "Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
