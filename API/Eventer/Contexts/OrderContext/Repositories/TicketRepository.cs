using Eventer.Contexts.OrderContext.Interfaces;
using Eventer.Database;
using Schema = Eventer.Database.Schemas;

namespace Eventer.Contexts.OrderContext.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Entities.Ticket ticket)
        {
            var ticketSchema = new Schema.Ticket
            {
                EventId = ticket.EventId,
                Code = ticket.Code
            };

            _context.Tickets.Add(ticketSchema);
            _context.SaveChanges();

            ticket.Id = ticketSchema.Id;
        }
    }
}
