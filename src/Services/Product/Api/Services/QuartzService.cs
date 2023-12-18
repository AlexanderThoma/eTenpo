using eTenpo.Product.Infrastructure.BackgroundJobs;
using Quartz;

namespace eTenpo.Product.Api.Services;

public static class QuartzService
{
    // Background job inside the same container (basically the same as Hangfire)
    public static IServiceCollection AddQuartzServices(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            options.UseInMemoryStore();
            
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            options.AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(
                    trigger => trigger.ForJob(jobKey)
                        .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10).RepeatForever()));
        });
        
        services.AddQuartzHostedService();

        return services;
    }
}