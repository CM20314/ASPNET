using CM20314.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CM20314.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Building> Building { get; set; }
        public DbSet<Node> Node { get; set; }
        public DbSet<NodeArc> NodeArc { get; set; }
    }
}
