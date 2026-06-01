using Eventer.Contexts.EventContext.DTOs.Requests;
using Eventer.Contexts.TicketContext.Interfaces;
using Eventer.Contexts.TicketContext.UseCases;
using Eventer.Database;
using Eventer.Database.Schemas;

namespace Eventer.Contexts.TicketContext.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(OrderContext.Entities.Ticket domainTicket)
        {
            var schemaTicket = new Ticket
            {
                EventId = domainTicket.EventId,
                Code = domainTicket.Code
            };
            _context.Tickets.Add(schemaTicket);
            _context.SaveChanges();
            domainTicket.Id = schemaTicket.Id;
        }

        public void Delete(int id)
        {
            if (!IsInDatabase(id)) throw new Exception("could not find ticket!");

            var ticket = FindById(id);

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
        }

        public Ticket FindById(int id)
        {
            if (!IsInDatabase(id)) throw new Exception("could not find ticket!");
            return _context.Tickets.First(t => t.Id == id);
        }

        public List<Ticket> GetAll()
        {
            return _context.Tickets.Select(t => new Ticket
            {
                Id = t.Id,
                Event = t.Event,
                EventId = t.EventId,
                Order = t.Order,
                Code = t.Code,
            }).ToList();
        }

        public bool IsInDatabase(int id)
        {
            return _context.Tickets.Any(t => t.Id == id);
        }

        public void Update(TicketUpdateCase update)
        {
            if (!IsInDatabase(update.Id)) throw new Exception("ticket is not in database");

            var ticket = FindById(update.Id);

            ticket.

            // transform Schema.Event to Entities.Event 
            ticket.Event = new Event
                {
                    Id = update.Id,
                    Name = update.Event.Name,
                    Capacity = update.Event.Capacity,
                    Description = update.Event.Description,
                    Location = update.Event.Location,
                    Price = update.Event.Price,
                    Date = update.Event.Date
                };
            ticket.Event.Id = update.Id;
            ticket.Code = update.Code;
            ticket.Order = update.Order;

            _context.SaveChanges();
        }
    }
}
