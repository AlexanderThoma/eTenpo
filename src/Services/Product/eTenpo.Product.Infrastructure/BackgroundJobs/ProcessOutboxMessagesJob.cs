using eTenpo.Product.Domain.Common;
using eTenpo.Product.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace eTenpo.Product.Infrastructure.BackgroundJobs;


// TODO: in-process handling of outbox messages (to be reimplemented in Azure functions)
[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext dbContext;
    private readonly IPublisher publisher;
    private readonly ILogger<ProcessOutboxMessagesJob> logger;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher, ILogger<ProcessOutboxMessagesJob> logger)
    {
        this.dbContext = dbContext;
        this.publisher = publisher;
        this.logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await dbContext.Set<OutboxMessage>().Where(x => x.ProcessedOnUtc == null).Take(20).ToListAsync();

        foreach (var outboxMessage in messages)
        {
            var domainEvent = JsonConvert.DeserializeObject<DomainEvent>(outboxMessage.Content);

            if (domainEvent is null)
            {
                this.logger.LogWarning("An empty domain event was found during processing. OutboxMessage: {@Message}", outboxMessage);
                continue;
            }

            try
            {
                await publisher.Publish(domainEvent, context.CancellationToken);
                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }
            catch (Exception exception)
            {
                this.logger.LogError("An error {@Error} occurred during the processing of outbox messages", exception);
                
                outboxMessage.ProcessedOnUtc = null;
                outboxMessage.Error = exception.Message;
            }
        }

        await dbContext.SaveChangesAsync();
    }
}