using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler
    (IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        // Publish the OrderCreatedIntegrationEvent to the message bus if feature flag is enabled.
        if(await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            logger.LogInformation("Publishing OrderCreatedIntegrationEvent for Order ID: {OrderId}", domainEvent.order.Id);
            var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }    
        else
        {
            logger.LogInformation("Feature 'PublishOrderCreatedIntegrationEvent' is disabled. Skipping event publishing.");
            return;
        }
    }
}
