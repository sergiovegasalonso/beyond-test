﻿using TodoLists.Domain.Entities;

namespace TodoLists.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TodoItem, LookupDto>();
        }
    }
}
