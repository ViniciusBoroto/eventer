using Eventer.Contexts.EventContext.DTOs.Requests;
using Eventer.Contexts.EventContext.Entities;

namespace Eventer.Contexts.EventContext.Interfaces
{
    public interface IEventRepository
    {
        public bool IsInDatabase(int id);
        public Event FindById(int id);
        public List<Event> GetAll();
        public void Update(Event eventEntity);
        public void Add(Event eventEntity);
        public void Delete(int id);
    }
}
