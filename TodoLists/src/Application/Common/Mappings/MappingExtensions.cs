﻿using TodoLists.Application.Common.Models;

namespace TodoLists.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, CancellationToken cancellationToken = default) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);
}
