using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eTenpo.Product.Infrastructure;

/// <summary>
/// To be able to use this class, remove all direct and transitive dependencies of EF core
/// which have an older release than the version used here.
/// Github issue: https://github.com/dotnet/efcore/issues/26749
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var connectionString = configuration.GetRequiredSection("ConnectionStrings")["SqlServer"];
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(1).TotalSeconds));
        
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}