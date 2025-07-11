namespace Catalog.API.Products.CreateProduct;


public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Business logic to create a product

        //create product entity from command object

        var result = await validator.ValidateAsync(command, cancellationToken);

        var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
        if (errors.Any())
        {
            throw new ValidationException(errors.FirstOrDefault());
        }

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        //todo: save to db
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        //return CreateProductResult result
        return new CreateProductResult(product.Id);
    }
}
