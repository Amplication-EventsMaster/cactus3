using EventManagement.APIs;
using EventManagement.Infrastructure;
using EventManagement.APIs.Dtos;
using EventManagement.Infrastructure.Models;
using EventManagement.APIs.Errors;
using EventManagement.APIs.Extensions;
using EventManagement.APIs.Common;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly EventManagementDbContext _context;
    public CustomersServiceBase(EventManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    public async Task<Customer> CreateCustomer(CustomerCreateInput createDto)
    {
        var customer = new CustomerDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName
        };

        if (createDto.Id != null)
        {
            customer.Id = createDto.Id;
        }
        if (createDto.Events != null)
        {
            customer.Events = await _context
                .Events.Where(event => createDto.Events.Select(t => t.Id).Contains(event.Id))
                    .ToListAsync();
            }

_context.Customers.Add(customer);
            await _context.SaveChangesAsync();

var result = await _context.FindAsync<CustomerDbModel>(customer.Id);
    
            if (result == null)
            {
                throw new NotFoundException();
            }
    
            return result.ToDto();}

    /// <summary>
    /// Connect multiple events records to Customer
    /// </summary>
    public async Task ConnectEvents(CustomerWhereUniqueInput uniqueId, EventWhereUniqueInput[] eventsId)
{
    var customer = await _context
        .Customers.Include(x => x.Events)
.FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (customer == null)
    {
        throw new NotFoundException();
    }

    var events = await _context
      .Events.Where(t => eventsId.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();
    if (events.Count == 0)
    {
        throw new NotFoundException();
    }

    var eventsToConnect = events.Except(customer.Events);

    foreach (var event in eventsToConnect){
        customer.Events.Add(event);
    }

    await _context.SaveChangesAsync();
}

/// <summary>
/// Disconnect multiple events records from Customer
/// </summary>
public async Task DisconnectEvents(CustomerWhereUniqueInput uniqueId, EventWhereUniqueInput[] eventsId)
{
    var customer = await _context
                          .Customers.Include(x => x.Events)
                  .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (customer == null)
    {
        throw new NotFoundException();
    }

    var events = await _context
      .Events.Where(t => eventsId.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();

    foreach (var event in events)
    {
        customer.Events?.Remove(event);
    }
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find multiple events records for Customer
/// </summary>
public async Task<List<Event>> FindEvents(CustomerWhereUniqueInput uniqueId, EventFindManyArgs customerFindManyArgs)
{
    var events = await _context
        .Events
.Where(m => m.CustomerId == uniqueId.Id)
.ApplyWhere(customerFindManyArgs.Where)
.ApplySkip(customerFindManyArgs.Skip)
.ApplyTake(customerFindManyArgs.Take)
.ApplyOrderBy(customerFindManyArgs.SortBy)
.ToListAsync();

    return events.Select(x => x.ToDto()).ToList();
}

/// <summary>
/// Meta data about Customer records
/// </summary>
public async Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs)
{

    var count = await _context
.Customers
.ApplyWhere(findManyArgs.Where)
.CountAsync();

    return new MetadataDto { Count = count };
}

/// <summary>
/// Update multiple events records for Customer
/// </summary>
public async Task UpdateEvents(CustomerWhereUniqueInput uniqueId, EventWhereUniqueInput[] eventsId)
{
    var customer = await _context
          .Customers.Include(t => t.Events)
  .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (customer == null)
    {
        throw new NotFoundException();
    }

    var events = await _context
      .Events.Where(a => eventsId.Select(x => x.Id).Contains(a.Id))
      .ToListAsync();

    if (events.Count == 0)
    {
        throw new NotFoundException();
    }

    customer.Events = events;
    await _context.SaveChangesAsync();
}

/// <summary>
/// Delete one Customer
/// </summary>
public async Task DeleteCustomer(CustomerWhereUniqueInput uniqueId)
{
    var customer = await _context.Customers.FindAsync(uniqueId.Id);
    if (customer == null)
    {
        throw new NotFoundException();
    }

    _context.Customers.Remove(customer);
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find many Customers
/// </summary>
public async Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs)
{
    var customers = await _context
        .Customers
.Include(x => x.Events)
.ApplyWhere(findManyArgs.Where)
.ApplySkip(findManyArgs.Skip)
.ApplyTake(findManyArgs.Take)
.ApplyOrderBy(findManyArgs.SortBy)
.ToListAsync();
    return customers.ConvertAll(customer => customer.ToDto());
}

/// <summary>
/// Get one Customer
/// </summary>
public async Task<Customer> Customer(CustomerWhereUniqueInput uniqueId)
{
    var customers = await this.Customers(
            new CustomerFindManyArgs { Where = new CustomerWhereInput { Id = uniqueId.Id } }
);
    var customer = customers.FirstOrDefault();
    if (customer == null)
    {
        throw new NotFoundException();
    }

    return customer;
}

/// <summary>
/// Update one Customer
/// </summary>
public async Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto)
{
    var customer = updateDto.ToModel(uniqueId);

    if (updateDto.Events != null)
    {
        customer.Events = await _context
            .Events.Where(event => updateDto.Events.Select(t => t).Contains(event.Id))
            .ToListAsync();
    }

    _context.Entry(customer).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Customers.Any(e => e.Id == customer.Id))
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
