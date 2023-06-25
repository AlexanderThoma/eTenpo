using eTenpo.Product.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eTenpo.Product.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Domain.AggregateRoots.ProductAggregate.Product>
{
    public void Configure(EntityTypeBuilder<Domain.AggregateRoots.ProductAggregate.Product> builder)
    {
        builder.ToTable(TableNames.Products);
        
        builder.HasKey(c => c.Id);
        
        builder.HasIndex(c => c.Name).IsUnique();

        builder.Ignore(x => x.DomainEvents);

        /*

        builder.Property(c => c.Id).HasConversion(
            customerId => customerId.Value,
            value => new CustomerId(value));

        builder.Property(c => c.Name).HasMaxLength(100);

        builder.Property(c => c.Email).HasMaxLength(255);

        */
    }
}