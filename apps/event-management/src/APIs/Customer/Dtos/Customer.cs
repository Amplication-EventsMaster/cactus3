namespace EventManagement.APIs.Dtos;

public class Customer
{
    public string Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public List<string>? Events { get; set; }
}
