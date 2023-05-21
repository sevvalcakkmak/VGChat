using Microsoft.EntityFrameworkCore;
using VGChat.Models;

namespace VGChat.Data
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Message> Messages { get; set; }
    }
}
