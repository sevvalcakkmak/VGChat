using Microsoft.EntityFrameworkCore;
using VGChat_demo.Models;

namespace VGChat_demo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Message> Messages { get; set; }
    }
}
