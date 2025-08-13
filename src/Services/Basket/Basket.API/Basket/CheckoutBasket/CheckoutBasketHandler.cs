using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) 
    : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto cannot be empty");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("Username cannot be empty");
    }
}

public class CheckoutBasketCommandHandler
    (IBasketRepository repository,IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        //get existing basket with total price
        var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        if (basket == null)
        {
            return new CheckoutBasketResult(false);
        }

        // Set total price on basket checkout event message
        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        // send basket checkout event to RabbitMQ using MassTransit
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        // delete the basket
        await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

        // return success result
        return new CheckoutBasketResult(true);
    }
}
