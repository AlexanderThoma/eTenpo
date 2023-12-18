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

        builder.Ignore(x => x.DomainEvents);
        
        // DDD comment: length restrictions or other validations are done inside the value objects
        // and should not be verified here
        
        
        builder.OwnsOne(c => c.Name, ownedNavigationBuilder =>
        {
            ownedNavigationBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Category.Name))
                .IsRequired();

            ownedNavigationBuilder.HasIndex(nameof(CategoryName.Value));
        });
        
        builder
            .OwnsOne(c => c.Description)
            .Property(p => p.Value)
            .HasColumnName(nameof(Category.Description));

        builder.Property(c => c.CreatedOnUtc).IsRequired();
        
        builder.Property(c => c.ModifiedOnUtc);
    }
}