using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Product.Test;

public abstract class BaseTestFixture : IClassFixture<IntegrationTestWebApplicationFactory>
{
    private readonly IServiceScope scope;
    protected readonly ISender Sender;
    
    protected BaseTestFixture(IntegrationTestWebApplicationFactory factory)
    {
        scope = factory.Services.CreateScope();
        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
    }
}