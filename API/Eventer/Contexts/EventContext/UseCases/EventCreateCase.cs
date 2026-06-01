using Eventer.Contexts.EventContext.DTOs.Requests;
using Eventer.Contexts.EventContext.Interfaces;
using Eventer.Contexts.EventContext.Entities;

namespace Eventer.Contexts.EventContext.UseCases
{
    public class EventCreateCase
    {
        private readonly IEventRepository _eventRepository;

        public EventCreateCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void Execute(CreateEventRequest createEventRequest)
        {
            var newEvent = new Event(
                createEventRequest.Name,
                createEventRequest.Description,
                createEventRequest.PictureUrl,
                createEventRequest.Price,
                createEventRequest.Capacity,
                createEventRequest.Date,
                createEventRequest.Location
            );

            _eventRepository.Add(newEvent);
        }
    }
}
