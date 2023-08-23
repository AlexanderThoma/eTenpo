using Ductus.FluentDocker.Services;
using eTenpo.Product.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Shared;

public sealed class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer sqlContainer;
    protected IHostService? dockerHost; 
    
    public IntegrationTestWebApplicationFactory()
    {
        EnsureDockerHost();
        
        sqlContainer = new MsSqlBuilder().Build();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        
        builder.ConfigureTestServices(services =>
        {
            var descriptor =
                services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(sqlContainer.GetConnectionString()));
        });
        
        base.ConfigureWebHost(builder);
    }

    public async Task InitializeAsync()
    {
        await sqlContainer.StartAsync();
    }
    
    public new async Task DisposeAsync()
    {
        await sqlContainer.StopAsync();
    }

    private void EnsureDockerHost()
    {
        var loopCount = 0;
        while (true)
        {
            if (dockerHost?.State == ServiceRunningState.Running)
            {
                return;
            }

            if (loopCount > 5)
            {
                throw new Exception("EnsureDockerHost was not successful");
            }

            var hosts = new Hosts().Discover();
            dockerHost = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");

            if (dockerHost is not null)
            {
                if (dockerHost.State is not ServiceRunningState.Running) dockerHost.Start();

                return;
            }

            if (hosts.Count > 0)
            {
                dockerHost = hosts.First();
            }

            if (dockerHost is not null)
            {
                return;
            }

            loopCount++;
        }
    }
}