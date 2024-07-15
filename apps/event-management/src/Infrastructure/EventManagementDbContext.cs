using EventManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrastructure;

public class EventManagementDbContext : IdentityDbContext<IdentityUser>
{
    public EventManagementDbContext(DbContextOptions<EventManagementDbContext> options)
        : base(options) { }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<EventDbModel> Events { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
