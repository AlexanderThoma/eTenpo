using eTenpo.Product.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Polly;
using Serilog;

namespace eTenpo.Product.Api.Services;

public static class MigrationService
{
    public static void Migrate(WebApplication app)
    {
        Policy retryPolicy = Policy.Handle<Exception>().WaitAndRetry(
            5,
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            (exception, timeSpan, context) => { Log.Error(exception, "Database is currently not available"); });

        retryPolicy.Execute(
            () =>
            {
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            });
    }
}