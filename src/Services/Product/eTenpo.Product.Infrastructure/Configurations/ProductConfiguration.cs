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
        
        builder.HasIndex(c => c.Name).IsUnique();

        builder.Ignore(x => x.DomainEvents);
        
        // DDD comment: length restrictions or other validations are done inside the value objects
        // and should not be verified here
        
        // add conversion for value objects, because ef core cannot do the mapping by itself

        builder.Property(c => c.Name)
            .IsRequired()
            .HasConversion(prop => prop.Value,
                value => new Name(value));
        
        builder.Property(c => c.Price)
            .HasConversion(prop => prop.Value,
            value => new Price(value));
        
        builder.Property(c => c.Description)
            .HasConversion(prop => prop.Value,
            value => new Description(value));
        
        builder.Property(c => c.AvailableStock)
            .HasConversion(prop => prop.Value,
            value => new Stock(value));
        
        builder.Property(c => c.CategoryId)
            .IsRequired()
            .HasConversion(prop => prop.Value,
            value => new CategoryId(value));
        
        builder.HasOne(a => a.Category)
            .WithMany()
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Property(c => c.CreatedOnUtc).IsRequired();
        
        builder.Property(c => c.ModifiedOnUtc);
    }
}