using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<WeatherStation> WeatherStation { get; set; }
        public DbSet<TemperatureMeasurement> TemperatureMeasurement { get; set; }
        public DbSet<ReadLog> ReadLog { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TemperatureMeasurement>().Property(t => t.Temperature).HasColumnType("decimal(9,4)").IsRequired(true);
        }
    }
}
