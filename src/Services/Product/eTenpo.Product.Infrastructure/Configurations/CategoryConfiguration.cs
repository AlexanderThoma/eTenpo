using eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;
using eTenpo.Product.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eTenpo.Product.Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(TableNames.Categories);
        
        builder.HasKey(c => c.Id);
        
        builder.HasIndex(c => c.Name).IsUnique();

        builder.Ignore(x => x.DomainEvents);
        
        // DDD comment: length restrictions or other validations are done inside the value objects
        // and should not be verified here
        
        // add conversion for value objects, because ef core cannot do the mapping by itself

        builder.Property(c => c.Name)
            .IsRequired()
            .HasConversion(prop => prop.Value,
                value => new CategoryName(value));
        
        builder.Property(c => c.Description)
            .HasConversion(prop => prop.Value,
            value => new CategoryDescription(value));

        builder.Property(c => c.CreatedOnUtc).IsRequired();
        
        builder.Property(c => c.ModifiedOnUtc);
    }
}