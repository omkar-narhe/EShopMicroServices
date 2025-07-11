namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{

    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
            .Length(2,150).WithMessage("Name must be between 2 and 150 characters.");

        RuleFor(x => x.Id).NotEmpty().WithMessage("ProdutId is required");

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater than zero.");

    }

}

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", 
            async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
    }
}
