﻿
using Discount.Grpc;

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
    (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        // Store basket in DB (Use Marten Upsert - if exist = update, if not = add
        await repository.StoreBasket(command.Cart, cancellationToken);
        //Todo: Update cache

        // For now, we will just return success
        return new StoreBasketResult(command.Cart.Username);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellation)
    {
        //Communicate with discount.grpc and calculate latest prices of products into shopping cart
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest() { ProductName = item.ProductName });
            item.Price -= Convert.ToDecimal(coupon.Amount);
        }
    }
}
