using EventManagement.APIs;
using EventManagement.Infrastructure;
using EventManagement.APIs.Dtos;
using EventManagement.Infrastructure.Models;
using EventManagement.APIs.Errors;
using EventManagement.APIs.Extensions;
using EventManagement.APIs.Common;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.APIs;

public abstract class EventsServiceBase : IEventsService
{
    protected readonly EventManagementDbContext _context;
    public EventsServiceBase(EventManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one event
    /// </summary>
    public async Task<Event> CreateEvent(EventCreateInput createDto)
    {
        var event = new EventDbModel
                  {
              CreatedAt = createDto.CreatedAt,
UpdatedAt = createDto.UpdatedAt,
Date = createDto.Date,
Name = createDto.Name
};
    
          if (createDto.Id != null){
            event.Id = createDto.Id;
}
          if (createDto.Customer != null)
          {
              event.Customer = await _context
                  .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                  .FirstOrDefaultAsync();
}

_context.Events.Add(event);
await _context.SaveChangesAsync();

var result = await _context.FindAsync<EventDbModel>(event.Id);
    
            if (result == null)
            {
    throw new NotFoundException();
}
    
            return result.ToDto();
}

/// <summary>
/// Delete one event
/// </summary>
public async Task DeleteEvent(EventWhereUniqueInput uniqueId)
{
    var event = await _context.Events.FindAsync(uniqueId.Id);
    if (event == null)
      {
        throw new NotFoundException();
    }

    _context.Events.Remove(event);
    await _context.SaveChangesAsync();
}

/// <summary>
/// Get a customer record for event
/// </summary>
public async Task<Customer> GetCustomer(EventWhereUniqueInput uniqueId)
{
    var event = await _context
        .Events.Where(event => event.Id == uniqueId.Id)
.Include(event => event.Customer)
.FirstOrDefaultAsync();
    if (event == null)
{
        throw new NotFoundException();
    }
    return event.Customer.ToDto();
}

/// <summary>
/// Meta data about event records
/// </summary>
public async Task<MetadataDto> EventsMeta(EventFindManyArgs findManyArgs)
{

    var count = await _context
.Events
.ApplyWhere(findManyArgs.Where)
.CountAsync();

    return new MetadataDto { Count = count };
}

/// <summary>
/// Find many events
/// </summary>
public async Task<List<Event>> Events(EventFindManyArgs findManyArgs)
{
    var events = await _context
        .Events
.Include(x => x.Customer)
.ApplyWhere(findManyArgs.Where)
.ApplySkip(findManyArgs.Skip)
.ApplyTake(findManyArgs.Take)
.ApplyOrderBy(findManyArgs.SortBy)
.ToListAsync();
    return events.ConvertAll(event => event.ToDto());
}

/// <summary>
/// Get one event
/// </summary>
public async Task<Event> Event(EventWhereUniqueInput uniqueId)
{
    var events = await this.events(
            new EventFindManyArgs { Where = new EventWhereInput { Id = uniqueId.Id } }
);
    var event = events.FirstOrDefault();
    if (event == null)
    {
        throw new NotFoundException();
    }

    return event;
}

/// <summary>
/// Update one event
/// </summary>
public async Task UpdateEvent(EventWhereUniqueInput uniqueId, EventUpdateInput updateDto)
{
    var event = updateDto.ToModel(uniqueId);



    _context.Entry(event).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Events.Any(e => e.Id == event.Id))
        {
            throw new NotFoundException();
        }
        else
        {
            throw;
        }
    }
}

}
