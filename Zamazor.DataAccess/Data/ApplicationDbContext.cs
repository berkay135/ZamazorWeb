using Microsoft.EntityFrameworkCore;
using Zamazor.Models;

namespace ZamazorWeb.DataAccess.Data
{
    //Configuration for entity framework core
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
               
        }

        //Create table named "Categories"
        public DbSet<Category> Categories { get; set; }

        //Seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1},
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
        }
    }
}
