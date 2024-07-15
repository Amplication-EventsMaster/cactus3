using Microsoft.AspNetCore.Mvc;

namespace EventManagement.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
