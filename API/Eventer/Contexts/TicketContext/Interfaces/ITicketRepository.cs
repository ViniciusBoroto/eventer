using Eventer.Contexts.EventContext.DTOs.Requests;
using Eventer.Contexts.TicketContext.UseCases;
using Eventer.Database.Schemas;

namespace Eventer.Contexts.TicketContext.Interfaces
{
    public interface ITicketRepository
    {
        public bool IsInDatabase(int id);
        public Ticket FindById(int id);
        public List<Ticket> GetAll();
        public void Update(TicketUpdateCase updateEvent);
        public void Add(Ticket ticket);
        public void Delete(int id);
    }
}
