using CM20314.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CM20314.Data
{
    public class ApplicationDbContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public DbSet<Entity> Entity { get; set; }
        public DbSet<Coordinate> Coordinate { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Floor> Floor { get; set; }
        public DbSet<Container> Container { get; set; }
        public DbSet<Node> Node { get; set; }
        public DbSet<NodeArc> NodeArc { get; set; }
    }
}
