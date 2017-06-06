using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MyDbContext: DbContext
    {
        public DbSet<WatherStation> WatherStation { get; set; }
        public DbSet<TemperatureMeasurement> TemperatureMeasurement { get; set; }
        public DbSet<ReadLog> ReadLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
           optionsBuilder.UseSqlServer(@"Server=DBTRUNKS\SQLEXPRESS;Database=WatherStation;Trusted_Connection=True;");
        }

        protected override void   OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TemperatureMeasurement>().Property(t => t.Temperature).HasColumnType("decimal(9,4)").IsRequired(true);
        }
    }
}
