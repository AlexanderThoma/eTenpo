using eTenpo.Product.Domain.AggregateRoots.ProductAggregate;
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

        builder.Ignore(x => x.DomainEvents);
        
        // DDD comment: length restrictions or other validations are done inside the value objects
        // and should not be verified here
        
        // add conversion for value objects, because ef core cannot do the mapping by itself

        builder.OwnsOne(c => c.ProductName, ownedNavigationBuilder =>
        {
            ownedNavigationBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Domain.AggregateRoots.ProductAggregate.Product.ProductName))
                .IsRequired();

            ownedNavigationBuilder.HasIndex(nameof(ProductName.Value));
        });
        
        builder
            .OwnsOne(c => c.ProductDescription)
            .Property(p => p.Value)
            .HasColumnName(nameof(Domain.AggregateRoots.ProductAggregate.Product.ProductDescription));
        
        builder
            .OwnsOne(c => c.Price)
            .Property(p => p.Value)
            .HasPrecision(19, 4)
            .HasColumnName(nameof(Domain.AggregateRoots.ProductAggregate.Product.Price));
        
        builder
            .OwnsOne(c => c.AvailableStock)
            .Property(p => p.Value)
            .HasColumnName(nameof(Domain.AggregateRoots.ProductAggregate.Product.AvailableStock));
        
        builder.HasOne(a => a.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(c => c.CreatedOnUtc).IsRequired();
        
        builder.Property(c => c.ModifiedOnUtc);
    }
}