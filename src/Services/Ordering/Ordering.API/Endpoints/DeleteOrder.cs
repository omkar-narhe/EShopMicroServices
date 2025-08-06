
using Ordering.Application.Orders.Commands.DeleteOrder;
using Ordering.Domain.ValueObjects;

namespace Ordering.API.Endpoints;

public record DeleteOrderResponse(bool IsSuccess);
public class DeleteOrder
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id:guid}", async (Guid Id, ISender sender, ILogger<DeleteOrder> logger) =>
        {
            var command = new DeleteOrderCommand(Id);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteOrderResponse>();
            return Results.Ok(response);
        })
            .WithName("DeleteOrder")
            .WithSummary("Delete an order by ID")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithTags("Orders");
    }
}
