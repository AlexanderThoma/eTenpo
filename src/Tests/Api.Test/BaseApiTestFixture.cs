using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Xunit;

namespace Product.Api.Test;

public abstract class BaseApiTestFixture : IClassFixture<IntegrationTestWebApplicationFactory>
{
    private readonly IServiceScope scope;
    //protected readonly Ihttp Sender;
    
    protected BaseApiTestFixture(IntegrationTestWebApplicationFactory factory)
    {
        scope = factory.Services.CreateScope();
        //Sender = scope.ServiceProvider.GetRequiredService<ISender>();
    }
}