﻿using MassTransit;

namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler
    (IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();

        await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
    }
}
