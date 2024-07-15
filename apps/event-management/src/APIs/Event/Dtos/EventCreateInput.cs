namespace EventManagement.APIs.Dtos;

public class EventCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? Date { get; set; }

    public string? Name { get; set; }

    public Customer? Customer { get; set; }
}
