using Microsoft.EntityFrameworkCore;

namespace eTenpo.Product.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // TODO: DateTimeKind handling missing
    // easy but not elegant solution would be to specify each dateTimeKind as UTC after retrieving from database
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
}