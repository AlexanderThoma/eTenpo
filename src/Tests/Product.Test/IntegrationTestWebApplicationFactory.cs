using Ductus.FluentDocker.Services;
using eTenpo.Product.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Product.Test;

public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer sqlContainer;
    protected IHostService? DockerHost; 
    
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
            if (DockerHost?.State == ServiceRunningState.Running)
            {
                return;
            }

            if (loopCount > 5)
            {
                throw new Exception("EnsureDockerHost was not successful");
            }

            var hosts = new Hosts().Discover();
            DockerHost = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");

            if (DockerHost is not null)
            {
                if (DockerHost.State is not ServiceRunningState.Running) DockerHost.Start();

                return;
            }

            if (hosts.Count > 0)
            {
                DockerHost = hosts.First();
            }

            if (DockerHost is not null)
            {
                return;
            }

            loopCount++;
        }
    }
}