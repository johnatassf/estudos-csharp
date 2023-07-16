
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using model.Entities;
using Model.Entities;
using System.Reflection.Metadata;

namespace infrastructure
{
    public class ApiContext : IdentityDbContext<User, IdentityRole<long>, long>
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

            modelBuilder.Entity<IdentityUserLogin<long>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<long>>().HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<IdentityUserToken<long>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            modelBuilder.Entity<IdentityUserClaim<long>>().HasKey(c => c.Id);

        }

    }
}
