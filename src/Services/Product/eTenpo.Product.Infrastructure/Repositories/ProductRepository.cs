using eTenpo.Product.Domain.Contracts;

namespace eTenpo.Product.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public ProductRepository(ProductDbContext dbContext)
    {
        
    }
}