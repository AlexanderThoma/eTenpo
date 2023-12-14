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
public class ProcessOutboxMessagesJob(
    ApplicationDbContext dbContext,
    IPublisher publisher,
    ILogger<ProcessOutboxMessagesJob> logger)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await dbContext.Set<OutboxMessage>().Where(x => x.ProcessedOnUtc == null).OrderBy(x => x.OccurredOnUtc).Take(20).ToListAsync();

        if (messages.Count == 0)
        {
            logger.LogInformation("Message queue is empty");
            
            return;
        }
        
        foreach (var outboxMessage in messages)
        {
            var domainEvent = JsonConvert.DeserializeObject<DomainEvent>(outboxMessage.Content);

            if (domainEvent is null)
            {
                logger.LogWarning("An empty domain event was found during processing. OutboxMessage: {@Message}", outboxMessage);
                continue;
            }

            try
            {
                await publisher.Publish(domainEvent, context.CancellationToken);
                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }
            catch (Exception exception)
            {
                logger.LogError("An error {@Error} occurred during the processing of outbox messages", exception);
                
                outboxMessage.ProcessedOnUtc = null;
                outboxMessage.Error = exception.Message;
            }
        }

        await dbContext.SaveChangesAsync();
    }
}