﻿
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerRequest(Guid CustomerId);

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customers/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var query = new GetOrdersByCustomerQuery(customerId);
            var result = await sender.Send(query);
            var response = result.Adapt<GetOrdersByCustomerResponse>();
            return Results.Ok(response);
        })
        .WithName("GetOrdersByCustomer")
        .WithSummary("Get orders by customer ID")
        .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}
