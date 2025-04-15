namespace TodoLists.Domain.Entities;

public class Progression : BaseEntity
{
    public DateTime Date { get; set; }

    public decimal Percent { get; set; }
}
