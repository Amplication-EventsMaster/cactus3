using EventManagement.Infrastructure;

namespace EventManagement.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(EventManagementDbContext context)
        : base(context) { }
}
