using Eventer.Contexts.OrderContext.Entities;
using Eventer.Contexts.TicketContext.UseCases;

namespace Eventer.Contexts.TicketContext.Interfaces
{
    public interface ITicketRepository
    {
        public bool IsInDatabase(int id);
        public Database.Schemas.Ticket FindById(int id);
        public List<Database.Schemas.Ticket> GetAll();
        public void Update(TicketUpdateCase updateEvent);
        public void Add(Ticket ticket);
        public void Delete(int id);
    }
}
