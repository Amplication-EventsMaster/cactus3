using EventManagement.APIs.Common;
using EventManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerFindManyArgs : FindManyInput<Customer, CustomerWhereInput> { }
