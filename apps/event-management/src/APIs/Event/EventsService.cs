using EventManagement.Infrastructure;

namespace EventManagement.APIs;

public class EventsService : EventsServiceBase
{
    public EventsService(EventManagementDbContext context)
        : base(context) { }
}
