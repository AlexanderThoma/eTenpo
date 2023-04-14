using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eTenpo.Product.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Domain.AggregateRoots.ProductAggregate.Product>
{
    public void Configure(EntityTypeBuilder<Domain.AggregateRoots.ProductAggregate.Product> builder)
    {
        throw new NotImplementedException();
    }
}