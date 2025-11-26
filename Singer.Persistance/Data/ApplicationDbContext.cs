using Microsoft.EntityFrameworkCore;

namespace Singer.Persistance.Data;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Singers;Trusted_Connection=True;");
        }
    }

    public DbSet<Entities.Singer> Singers { get; set; }
    public DbSet<Entities.SingerLabel> SingerLabels { get; set; }
}