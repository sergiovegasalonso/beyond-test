using System.ComponentModel.DataAnnotations.Schema;

namespace TodoLists.Domain.Entities;

public class Progression : BaseEntity
{
    public int TodoItemId { get; set; }

    public DateTime Date { get; set; }

    public decimal Percent { get; set; }
}
