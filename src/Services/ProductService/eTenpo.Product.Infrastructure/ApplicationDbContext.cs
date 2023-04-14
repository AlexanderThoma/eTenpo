using Microsoft.EntityFrameworkCore;

namespace eTenpo.Product.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Domain.AggregateRoots.ProductAggregate.Product> Products { get; set; }
    public DbSet<Domain.AggregateRoots.ProductAggregate.Product> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
}