using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Xunit;

namespace Product.Application.Test;

public abstract class BaseApplicationTestFixture : IClassFixture<IntegrationTestWebApplicationFactory>
{
    private readonly IServiceScope scope;
    protected readonly ISender Sender;
    
    protected BaseApplicationTestFixture(IntegrationTestWebApplicationFactory factory)
    {
        scope = factory.Services.CreateScope();
        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
    }
}