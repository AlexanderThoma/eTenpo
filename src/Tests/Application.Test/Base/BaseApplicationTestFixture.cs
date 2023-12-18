using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary;
using Xunit;

namespace Product.Application.Test.Base;

public abstract class BaseApplicationTestFixture : IClassFixture<IntegrationTestWebApplicationFactory>
{
    protected ISender Sender { get; }

    protected BaseApplicationTestFixture(IntegrationTestWebApplicationFactory factory)
    {
        var scope = factory.Services.CreateScope();
        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
    }
}
