using Eventer.Contexts.EventContext.DTOs.Requests;
using Eventer.Contexts.EventContext.Entities;
using Eventer.Contexts.EventContext.Interfaces;
using Eventer.Database;
using Schema = Eventer.Database.Schemas;

namespace Eventer.Contexts.EventContext.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public Event FindById(int id)
        {
            var eventSchema = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventSchema == null) throw new Exception("could not find event!");

            return new Event(
                eventSchema.Id,
                eventSchema.Name,
                eventSchema.Description,
                eventSchema.PictureUrl,
                eventSchema.Price,
                eventSchema.Capacity,
                eventSchema.Date,
                eventSchema.Location
            );
        }

        public List<Event> GetAll()
        {
            return _context.Events.Select(e => new Event(
                e.Id,
                e.Name,
                e.Description,
                e.PictureUrl,
                e.Price,
                e.Capacity,
                e.Date,
                e.Location
            )).ToList();
        }

        public void Update(Event eventEntity)
        {
            var eventSchema = _context.Events.FirstOrDefault(e => e.Id == eventEntity.Id);
            if (eventSchema == null) throw new Exception("could not find event!");

            eventSchema.Name = eventEntity.Name;
            eventSchema.Description = eventEntity.Description;
            eventSchema.PictureUrl = eventEntity.PictureUrl;
            eventSchema.Price = eventEntity.Price;
            eventSchema.Capacity = eventEntity.Capacity;
            eventSchema.Date = eventEntity.Date;
            eventSchema.Location = eventEntity.Location;

            _context.SaveChanges();
        }

        public void Add(Event eventEntity)
        {
            var eventSchema = new Schema.Event
            {
                Name = eventEntity.Name,
                Description = eventEntity.Description,
                PictureUrl = eventEntity.PictureUrl,
                Price = eventEntity.Price,
                Capacity = eventEntity.Capacity,
                Date = eventEntity.Date,
                Location = eventEntity.Location
            };

            _context.Events.Add(eventSchema);
            _context.SaveChanges();

            eventEntity.Id = eventSchema.Id;
        }

        public void Delete(int id)
        {
            var existing = _context.Events.FirstOrDefault(e => e.Id == id);
            if (existing == null) throw new Exception("could not find event to delete!");

            _context.Events.Remove(existing);
            _context.SaveChanges();
        }

        public bool IsInDatabase(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
