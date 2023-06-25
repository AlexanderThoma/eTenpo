using eTenpo.Product.Infrastructure.BackgroundJobs;
using Quartz;

namespace eTenpo.Product.Api.Services;

public static class QuartzService
{
    public static IServiceCollection AddQuartzServices(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(
                    trigger => trigger.ForJob(jobKey)
                        .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10).RepeatForever()));
    
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });
        
        services.AddQuartzHostedService();

        return services;
    }
}