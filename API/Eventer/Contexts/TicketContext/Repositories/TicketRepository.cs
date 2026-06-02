using Eventer.Contexts.TicketContext.Entities;
using Eventer.Contexts.TicketContext.Interfaces;
using Eventer.Database;

namespace Eventer.Contexts.TicketContext.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public Ticket FindById(int id)
        {
            var schema = _context.Tickets.FirstOrDefault(t => t.Id == id);
            if (schema == null) throw new Exception("could not find ticket!");

            return new Ticket(schema.Id, schema.EventId, schema.Code);
        }

        public void Create(Ticket ticket)
        {
            var schema = new Database.Schemas.Ticket
            {
                EventId = ticket.EventId,
                Code = ticket.Code
            };

            _context.Tickets.Add(schema);
            _context.SaveChanges();

            ticket.Id = schema.Id;
        }
    }
}
