using Microsoft.Extensions.DependencyInjection;
using SharedLibrary;
using Xunit;

namespace Product.Api.Test.Base;

// TODO: write base class for tests using http-client
public abstract class BaseApiTestFixture(IntegrationTestWebApplicationFactory factory)
    : IClassFixture<IntegrationTestWebApplicationFactory>
{
    private readonly IServiceScope scope = factory.Services.CreateScope();
    //protected readonly Ihttp Sender;
    //Sender = scope.ServiceProvider.GetRequiredService<ISender>();
}
