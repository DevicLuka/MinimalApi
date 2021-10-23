using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Motorbike> Motorbikes { get; set; }

        public DbSet<CarRace> CarRaces { get; set; }

        public DbSet<MotorbikeRace> MotorbikeRaces { get; set; }
    }
}
