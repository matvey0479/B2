using Microsoft.EntityFrameworkCore;
using Test.Domain.Entites;

namespace Test.Domain
{
    public class PolygonsDbContext:DbContext
    {
        public PolygonsDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Polygon> Polygons => Set<Polygon>();
        public DbSet<Point> Points => Set<Point>();
    }
}
