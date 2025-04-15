using TodoLists.Domain.Entities;

namespace TodoLists.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Progression> Progressions { get; }

    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
