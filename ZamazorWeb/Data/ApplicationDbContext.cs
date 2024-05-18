using Microsoft.EntityFrameworkCore;

namespace ZamazorWeb.Data
{
    //Congiguration for entity framework
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
               
        }
    }
}
