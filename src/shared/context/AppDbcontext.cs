using System;
using Microsoft.EntityFrameworkCore;
using MyInventory2026.src.modules.provider.Infrastructure.entity;

namespace MyInventory2026.src.shared.context;

public class AppDbContext : DbContext
{
    public DbSet<ProviderEntity> Providers => Set<ProviderEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}