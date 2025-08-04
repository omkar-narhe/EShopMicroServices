using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto order)
    : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator: AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.order.Id).NotEmpty().WithMessage("Order ID cannot be empty.");
        RuleFor(x => x.order.OrderName).NotEmpty().WithMessage("Order name cannot be empty.");
        RuleFor(x => x.order.CustomerId).NotEmpty().WithMessage("Customer ID cannot be empty.");
    }
}
