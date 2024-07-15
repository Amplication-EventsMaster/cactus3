using EventManagement.APIs.Dtos;
using EventManagement.Infrastructure.Models;

namespace EventManagement.APIs.Extensions;

public static class EventsExtensions
{
    public static Event ToDto(this EventDbModel model)
    {
        return new Event
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Date = model.Date,
            Name = model.Name,
            Customer = model.CustomerId,

        };
    }

    public static EventDbModel ToModel(this EventUpdateInput updateDto, EventWhereUniqueInput uniqueId)
    {
        var event = new EventDbModel { 
               Id = uniqueId.Id,
Date = updateDto.Date,
Name = updateDto.Name
};

     // map required fields
     if(updateDto.CreatedAt != null) {
     event.CreatedAt = updateDto.CreatedAt.Value;
}
if(updateDto.UpdatedAt != null) {
     event.UpdatedAt = updateDto.UpdatedAt.Value;
}

    return event; }

}
