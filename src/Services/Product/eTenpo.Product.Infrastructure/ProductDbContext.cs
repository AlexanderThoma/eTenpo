using Microsoft.EntityFrameworkCore;

namespace eTenpo.Product.Infrastructure;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }
}