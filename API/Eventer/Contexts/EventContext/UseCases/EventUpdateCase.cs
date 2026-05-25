using Eventer.Contexts.EventContext.DTOs.Requests;
using Eventer.Contexts.EventContext.Interfaces;
using Eventer.Contexts.EventContext.Entities;
using Eventer.Contexts.ValueObject;

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
            Event eventToUpdate = _eventRepository.FindById(updateEventRequest.Id);

            try
            {
                if (eventToUpdate == null) throw new Exception("event is null!");

                if (updateEventRequest.Price < 0.0m) throw new Exception("price cannot be negative!");

                if (updateEventRequest.Capacity <= 0) throw new Exception("capacity must be positive");

                new BaseStringObject(updateEventRequest.Name);
                new BaseStringObject(updateEventRequest.Description);
                new BaseStringObject(updateEventRequest.Location);
                new DateObject(updateEventRequest.Date);
            }
            catch (Exception)
            {
                throw;
            }

            eventToUpdate.Update(
                updateEventRequest.Name,
                updateEventRequest.Description,
                updateEventRequest.Price,
                updateEventRequest.Capacity,
                updateEventRequest.Date,
                updateEventRequest.Location
            );

            _eventRepository.Update(eventToUpdate);
        }
    }
}