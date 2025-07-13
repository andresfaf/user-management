using Microsoft.EntityFrameworkCore;
using UserManagement.Data.Entities;

namespace UserManagement.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(u => u.BirthDate)
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(u => u.Gender)
                    .HasColumnType("char(1)")
                    .IsRequired();
            });
        }
    }
}
