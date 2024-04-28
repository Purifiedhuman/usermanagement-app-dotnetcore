using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserService.Data.Interfaces;
using UserService.Entities;

namespace UserService.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var createdDateProperty = entityType.FindProperty("CreatedAt");
            if (createdDateProperty != null && createdDateProperty.ClrType == typeof(DateTime))
            {
                modelBuilder.Entity(entityType.ClrType).Property<DateTime>("CreatedAt")
                    .HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();

                modelBuilder.Entity(entityType.ClrType).Property<DateTime>("UpdatedAt")
                    .HasDefaultValueSql("NOW()").ValueGeneratedOnAddOrUpdate();
            }
        }

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    public DbSet<UserTask> UserTasks { get; set; }
}
