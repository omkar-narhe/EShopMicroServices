﻿
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        // Retrieve orders from the database with pagination
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Orders
            .AsNoTracking()
            .LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .OrderBy(o => o.OrderName.Value)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        // return result
        return new GetOrdersResult
        (
            new PaginatedResult<OrderDto>
            (
                pageIndex,
                pageSize,
                totalCount,
                orders.ToOrderDtoList()
            )
        );
    }
}
