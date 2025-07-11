namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryRequest(string Category): IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);

public class GetProductsByCategoryQueryHandler(IDocumentSession session) 
    : IQueryHandler<GetProductsByCategoryRequest, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryRequest query, CancellationToken cancellationToken)
    {
        var result = await session.Query<Product>()
            .Where(x => x.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductsByCategoryResult(result);
        
    }
}
