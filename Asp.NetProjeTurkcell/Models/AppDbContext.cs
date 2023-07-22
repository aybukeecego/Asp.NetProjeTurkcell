using Microsoft.EntityFrameworkCore;
using Asp.NetProjeTurkcell.Models;

namespace Asp.NetProjeTurkcell.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<Asp.NetProjeTurkcell.Models.Category>? Category { get; set; }
    }
}
