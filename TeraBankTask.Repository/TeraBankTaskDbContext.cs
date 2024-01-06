using Microsoft.EntityFrameworkCore;
using TeraBankTask.DTO;

namespace TeraBankTask.Repository;

public class TeraBankTaskDbContext(DbContextOptions<TeraBankTaskDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>()
            .Property(u => u.Balance)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<User>().Property(u => u.Password)
            .HasMaxLength(256);

        modelBuilder.Entity<UserAccount>().Property(u => u.Balance)
            .HasColumnType("money");

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }
}