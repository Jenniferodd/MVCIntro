using Microsoft.EntityFrameworkCore;
using MVC_Start.Models;

namespace MVC_Start.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Station> Stations { get; set; }
        public DbSet<Park> Parks { get; set; }
        public DbSet<StationParkDistance> StationParkDistances { get; set; }
    }
}