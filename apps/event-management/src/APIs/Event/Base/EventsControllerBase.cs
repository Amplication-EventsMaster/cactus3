using EventManagement.APIs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventManagement.APIs.Dtos;
using EventManagement.APIs.Errors;
using EventManagement.APIs.Common;

namespace EventManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EventsControllerBase : ControllerBase
{
    protected readonly IEventsService _service;
    public EventsControllerBase(IEventsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one event
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Event>> CreateEvent(EventCreateInput input)
    {
        var event = await _service.CreateEvent(input);
        
    return CreatedAtAction(nameof(Event), new { id = event.Id }, event); }

    /// <summary>
    /// Delete one event
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteEvent([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteEvent(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a customer record for event
    /// </summary>
    [HttpGet("{Id}/customers")]
    public async Task<ActionResult<List<Customer>>> GetCustomer([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }

    /// <summary>
    /// Meta data about event records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EventsMeta([FromQuery()]
    EventFindManyArgs filter)
    {
        return Ok(await _service.EventsMeta(filter));
    }

    /// <summary>
    /// Find many events
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Event>>> Events([FromQuery()]
    EventFindManyArgs filter)
    {
        return Ok(await _service.Events(filter));
    }

    /// <summary>
    /// Get one event
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Event>> Event([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Event(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one event
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateEvent([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    EventUpdateInput eventUpdateDto)
    {
        try
        {
            await _service.UpdateEvent(uniqueId, eventUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

}
