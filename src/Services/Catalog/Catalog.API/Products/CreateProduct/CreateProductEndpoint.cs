
namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record CreateProductResponse(Guid Id);


public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{

    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater than zero.");

    }

}

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
        
    }
}
