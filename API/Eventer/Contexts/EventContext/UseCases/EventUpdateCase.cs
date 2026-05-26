using Eventer.Contexts.EventContext.DTOs.Requests;
using Eventer.Contexts.EventContext.Interfaces;
using Eventer.Contexts.EventContext.Entities;

namespace Eventer.Contexts.EventContext.UseCases
{
    public class EventUpdateCase
    {
        private readonly IEventRepository _eventRepository;

        public EventUpdateCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void Execute(UpdateEventRequest updateEventRequest)
        {
            var eventToUpdate = _eventRepository.FindById(updateEventRequest.Id)
                ?? throw new Exception("event is null!");

            eventToUpdate.Update(
                updateEventRequest.Name,
                updateEventRequest.Description,
                updateEventRequest.PictureUrl,
                updateEventRequest.Price,
                updateEventRequest.Capacity,
                updateEventRequest.Date,
                updateEventRequest.Location
            );

            _eventRepository.Update(eventToUpdate);
        }
    }
}
