using futArabicom.Models;
using Microsoft.EntityFrameworkCore;

namespace futArabicom.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) { }

        public DbSet<Player> Players { get; set; }
    }
}
