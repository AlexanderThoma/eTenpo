using eTenpo.Product.Infrastructure.Outbox;
using eTenpo.Product.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eTenpo.Product.Infrastructure.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(TableNames.OutboxMessages);
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Type).IsRequired();

        builder.Property(c => c.Content).IsRequired();
    
        builder.Property(c => c.OccurredOnUtc).IsRequired();

        builder.Property(c => c.ProcessedOnUtc);
        
        builder.Property(c => c.Error);
    }
}