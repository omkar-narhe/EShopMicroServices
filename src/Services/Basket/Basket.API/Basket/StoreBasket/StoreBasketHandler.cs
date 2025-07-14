
namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart): ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator: AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
        RuleFor(x => x.Cart.Username).NotNull().WithMessage("Username is required.");
    }
}

public class StoreBasketCommandHandler
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        //Todo: Store the basket in the database (Use marten upserrt)
        //Todo: Update cache

        // For now, we will just return success
        return Task.FromResult(new StoreBasketResult("swn"));
    }
}
