using api.dotnet.help_us.Modules.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.dotnet.help_us.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<UserEntity> Users { get; set; }
    
    // Sobrescrevemos SaveChanges para atualizar o UpdatedAt
    // public override int SaveChanges()
    // {
    //     UpdateTimestamps();
    //     return base.SaveChanges();
    // }
    //
    // private void UpdateTimestamps()
    // {
    //     var entries = ChangeTracker.Entries<BaseEntity>()
    //         .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
    //
    //     foreach (var entry in entries)
    //     {
    //         entry.Entity.UpdatedAt = DateTime.UtcNow;
    //
    //         if (entry.State == EntityState.Added)
    //         {
    //             entry.Entity.CreatedAt = DateTime.UtcNow;
    //         }
    //     }
    // }
    
}