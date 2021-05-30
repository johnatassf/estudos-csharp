using EstudoAutenticacao.Model;
using Microsoft.EntityFrameworkCore;

namespace EstudoAutenticacao.dbcontext
{
    public class EstudoDbContext : DbContext
    {
        public EstudoDbContext(DbContextOptions<EstudoDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .UseMySqlIdentityColumn()
                    .IsRequired();

                entity.Property(e => e.Login)
                .HasMaxLength(100);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(250);

                entity.Property(e => e.Hash)
                   .IsRequired()
                   .HasMaxLength(255);

                entity.ToTable("user");
            });
    
        }
    }
}
