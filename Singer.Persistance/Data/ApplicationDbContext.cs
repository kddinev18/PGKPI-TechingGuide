using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Singer.Persistance.Data.Entities;

namespace Singer.Persistance.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
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