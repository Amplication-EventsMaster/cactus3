using EventManagement.APIs;

namespace EventManagement;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IEventsService, EventsService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
