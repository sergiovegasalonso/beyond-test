using TodoLists.Application.Common.Interfaces;

namespace TodoLists.Application.UseCases.GetItems;

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, List<TodoItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TodoItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        return await _context.TodoItems
            .OrderBy(x => x.Title)
            .ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
