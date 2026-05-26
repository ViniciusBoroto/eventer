using Eventer.Contexts.EventContext.DTOs.Requests;
using Eventer.Contexts.EventContext.Interfaces;
using Eventer.Contexts.EventContext.Entities;
using Eventer.Contexts.ValueObject;

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
            try
            {
                if (createEventRequest.Price < 0.0m) throw new Exception("price cannot be negative!");

                if (createEventRequest.Capacity <= 0) throw new Exception("capacity must be positive");

                new BaseStringObject(createEventRequest.Name);
                new BaseStringObject(createEventRequest.Description);
                new BaseStringObject(createEventRequest.PictureUrl);
                new BaseStringObject(createEventRequest.Location);
                new BaseStringObject(createEventRequest.Date.ToString());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            var newEvent = new Event
            {
                Name = createEventRequest.Name,
                Description = createEventRequest.Description,
                PictureUrl = createEventRequest.PictureUrl,
                Price = createEventRequest.Price,
                Capacity = createEventRequest.Capacity,
                Date = createEventRequest.Date,
                Location = createEventRequest.Location
            };

            _eventRepository.Add(newEvent);
        }
    }
}
