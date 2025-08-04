﻿
using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        // Retrieve orders by name from the database
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Contains(query.OrderName, StringComparison.OrdinalIgnoreCase))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);

        // Map the orders to the result DTO
        var orderDtos = ProjectToOrderDto(orders);
        // return result with list of orders
        return new GetOrdersByNameResult(orderDtos);
        ;
    }

    private List<OrderDto> ProjectToOrderDto(List<Order> orders)
    {
        List<OrderDto> orderDtos = new();
        foreach (var order in orders)
        {
            var orderDto = new OrderDto
            (
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDto
                (
                    FirstName: order.ShippingAddress.FirstName,
                    LastName: order.ShippingAddress.LastName,
                    EmailAddress: order.ShippingAddress.EmailAddress,
                    AddressLine: order.ShippingAddress.AddressLine,
                    Country : order.ShippingAddress.Country,
                    State: order.ShippingAddress.State,
                    ZipCode: order.ShippingAddress.ZipCode
                ),
                BillingAddress : new AddressDto
                (
                    FirstName : order.BillingAddress.FirstName,
                    LastName : order.BillingAddress.LastName,
                    EmailAddress : order.BillingAddress.EmailAddress,
                    AddressLine : order.BillingAddress.AddressLine,
                    Country: order.BillingAddress.Country,
                    State: order.BillingAddress.State,
                    ZipCode : order.BillingAddress.ZipCode
                ),
                Payment : new PaymentDto
                (
                    CardName : order.Payment.CardName,
                    CardNumber: order.Payment.CardNumber,
                    Expiration: order.Payment.ExpirationDate,
                    Cvv: order.Payment.CVV,
                    PaymentMethod : order.Payment.PaymentMethod
                ),
                Status: order.Status,
                OrderItems : order.OrderItems.Select(item => new OrderItemDto
                (
                    OrderId: item.OrderId.Value,
                    ProductId : item.ProductId.Value,
                    Quantity: item.Quantity,
                    Price: item.Price
                )).ToList()
            );

            orderDtos.Add(orderDto);
        }
        return orderDtos;
    }
}
