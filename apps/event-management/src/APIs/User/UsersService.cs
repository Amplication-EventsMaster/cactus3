using EventManagement.Infrastructure;

namespace EventManagement.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(EventManagementDbContext context)
        : base(context) { }
}
