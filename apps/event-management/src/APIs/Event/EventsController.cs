using Microsoft.AspNetCore.Mvc;

namespace EventManagement.APIs;

[ApiController()]
public class EventsController : EventsControllerBase
{
    public EventsController(IEventsService service)
        : base(service) { }
}
