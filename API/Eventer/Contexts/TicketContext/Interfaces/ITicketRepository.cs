using Eventer.Contexts.TicketContext.Entities;

namespace Eventer.Contexts.TicketContext.Interfaces
{
    public interface ITicketRepository
    {
        Ticket FindById(int id);
        void Create(Ticket ticket);
    }
}
