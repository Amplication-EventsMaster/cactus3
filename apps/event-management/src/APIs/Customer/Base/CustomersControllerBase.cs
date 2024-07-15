using EventManagement.APIs;
using EventManagement.APIs.Common;
using EventManagement.APIs.Dtos;
using EventManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Connect multiple events records to Customer
    /// </summary>
    [HttpPost("{Id}/events")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectEvents(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] EventWhereUniqueInput[] eventsId
    )
    {
        try
        {
            await _service.ConnectEvents(uniqueId, eventsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple events records from Customer
    /// </summary>
    [HttpDelete("{Id}/events")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectEvents(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] EventWhereUniqueInput[] eventsId
    )
    {
        try
        {
            await _service.DisconnectEvents(uniqueId, eventsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple events records for Customer
    /// </summary>
    [HttpGet("{Id}/events")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Event>>> FindEvents(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] EventFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindEvents(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Update multiple events records for Customer
    /// </summary>
    [HttpPatch("{Id}/events")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateEvents(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] EventWhereUniqueInput[] eventsId
    )
    {
        try
        {
            await _service.UpdateEvents(uniqueId, eventsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCustomer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Customer>>> Customers(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Customer>> Customer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Customer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(uniqueId, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
