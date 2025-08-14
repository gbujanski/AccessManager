using Microsoft.EntityFrameworkCore;
using AccessManager.Domain.Users;

namespace AccessManager.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        // SEED ADMIN
        var adminId = Guid.NewGuid();
        modelBuilder.Entity<User>().HasData(new User(
            adminId,
            "admin",
            BCrypt.Net.BCrypt.HashPassword("admin"),
            "admin"
        ));
    }
}

