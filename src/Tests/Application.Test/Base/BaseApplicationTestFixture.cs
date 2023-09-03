using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Xunit;

namespace Product.Application.Test.Base;

public abstract class BaseApplicationTestFixture : IClassFixture<IntegrationTestWebApplicationFactory>
{
    protected readonly ISender Sender;
    
    protected BaseApplicationTestFixture(IntegrationTestWebApplicationFactory factory)
    {
        var scope = factory.Services.CreateScope();
        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
    }
}