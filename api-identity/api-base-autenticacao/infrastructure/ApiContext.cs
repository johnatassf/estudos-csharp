
using Microsoft.EntityFrameworkCore;
using model.Entities;
using System.Reflection.Metadata;

namespace infrastructure
{
    public class ApiContext : DbContext
    {
        string _source = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=api-authentication;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public DbSet<WeatherForecast> weatherForecasts { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_source);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>()
                .HasKey(w => w.Id);
                
        }

    }
}
