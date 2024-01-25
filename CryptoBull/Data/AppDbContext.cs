
using Microsoft.EntityFrameworkCore;
using CryptoBull.Models;

namespace CryptoBull.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-GTHJBAB\\MSSQLSERVER01;Database=Users;Trusted_Connection=True;TrustServerCertificate=True;");
        }


    }
}
