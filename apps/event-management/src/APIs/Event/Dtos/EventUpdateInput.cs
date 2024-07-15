namespace EventManagement.APIs.Dtos;

public class EventUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? Date { get; set; }

    public string? Name { get; set; }

    public string? Customer { get; set; }
}
