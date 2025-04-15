using TodoLists.Application.Common.Models;

namespace TodoLists.Application.UseCases.GetItems;

public class GetItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}