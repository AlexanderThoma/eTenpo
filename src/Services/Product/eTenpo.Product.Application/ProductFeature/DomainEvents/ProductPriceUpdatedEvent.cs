using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Application.ProductFeature.DomainEvents;

public class ProductPriceUpdatedEvent : DomainEvent
{
    public decimal OldPrice { get; set; }

    public decimal NewPrice { get; set; }
}