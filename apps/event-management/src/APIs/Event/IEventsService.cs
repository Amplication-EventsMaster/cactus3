using EventManagement.APIs.Dtos;
using EventManagement.APIs.Common;

namespace EventManagement.APIs;

public interface IEventsService
{
    /// <summary>
    /// Create one event
    /// </summary>
    public Task<Event> CreateEvent(EventCreateInput event);
    /// <summary>
    /// Delete one event
    /// </summary>
    public Task DeleteEvent(EventWhereUniqueInput uniqueId);
    /// <summary>
    /// Get a customer record for event
    /// </summary>
    public Task<Customer> GetCustomer(EventWhereUniqueInput uniqueId);
    /// <summary>
    /// Meta data about event records
    /// </summary>
    public Task<MetadataDto> EventsMeta(EventFindManyArgs findManyArgs);
    /// <summary>
    /// Find many events
    /// </summary>
    public Task<List<Event>> Events(EventFindManyArgs findManyArgs);
    /// <summary>
    /// Get one event
    /// </summary>
    public Task<Event> Event(EventWhereUniqueInput uniqueId);
    /// <summary>
    /// Update one event
    /// </summary>
    public Task UpdateEvent(EventWhereUniqueInput uniqueId, EventUpdateInput updateDto);
}
