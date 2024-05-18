using Microsoft.EntityFrameworkCore;
using ZamazorWeb.Models;

namespace ZamazorWeb.Data
{
    //Congiguration for entity framework core
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
               
        }

        //Create table named "Categories"
        public DbSet<Category> Categories { get; set; }
    }
}
