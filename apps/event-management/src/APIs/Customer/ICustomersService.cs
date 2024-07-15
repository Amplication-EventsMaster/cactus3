using EventManagement.APIs.Common;
using EventManagement.APIs.Dtos;

namespace EventManagement.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Connect multiple events records to Customer
    /// </summary>
    public Task ConnectEvents(CustomerWhereUniqueInput uniqueId, EventWhereUniqueInput[] eventsId);

    /// <summary>
    /// Disconnect multiple events records from Customer
    /// </summary>
    public Task DisconnectEvents(
        CustomerWhereUniqueInput uniqueId,
        EventWhereUniqueInput[] eventsId
    );

    /// <summary>
    /// Find multiple events records for Customer
    /// </summary>
    public Task<List<Event>> FindEvents(
        CustomerWhereUniqueInput uniqueId,
        EventFindManyArgs EventFindManyArgs
    );

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple events records for Customer
    /// </summary>
    public Task UpdateEvents(CustomerWhereUniqueInput uniqueId, EventWhereUniqueInput[] eventsId);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);
}
