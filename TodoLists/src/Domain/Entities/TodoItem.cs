﻿namespace TodoLists.Domain.Entities;

public class TodoItem : BaseAuditableEntity
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }

    private bool _isCompleted;
    public bool IsCompleted
    {
        get => _isCompleted;
        private set => _isCompleted = value;
    }

    public virtual List<Progression>? Progressions { get; set; }
}
