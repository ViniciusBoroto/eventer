using Eventer.Contexts.OrderContext.Entities;
using Eventer.Contexts.OrderContext.Interfaces;
using Eventer.Database;
using Microsoft.EntityFrameworkCore;
using Schema = Eventer.Database.Schemas;

namespace Eventer.Contexts.OrderContext.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public Order FindById(int id)
        {
            var orderSchema = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (orderSchema == null) throw new Exception("could not find order!");

            return new Order
            {
                Id = orderSchema.Id,
                EventId = orderSchema.EventId,
                TicketId = orderSchema.TicketId,
                CreatedAt = orderSchema.CreatedAt,
                ConfirmedAt = orderSchema.ConfirmedAt,
                CanceledAt = orderSchema.CanceledAt
            };
        }

        public List<Order> GetAll()
        {
            return _context.Orders.Select(o => new Order
            {
                Id = o.Id,
                EventId = o.EventId,
                TicketId = o.TicketId,
                CreatedAt = o.CreatedAt,
                ConfirmedAt = o.ConfirmedAt,
                CanceledAt = o.CanceledAt
            }).ToList();
        }

        public void Add(Order orderEntity)
        {
            var orderSchema = new Schema.Order
            {
                EventId = orderEntity.EventId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(orderSchema);
            _context.SaveChanges();

            orderEntity.Id = orderSchema.Id;
            orderEntity.CreatedAt = orderSchema.CreatedAt;
        }

        public void Delete(int id)
        {
            var existing = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (existing == null) throw new Exception("could not find order to delete!");

            _context.Orders.Remove(existing);
            _context.SaveChanges();
        }

        public void Pay(int id)
        {
            var existing = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (existing == null) throw new Exception("could not find order!");

            if (existing.CanceledAt != null) throw new Exception("canceled orders cannot be paid!");
            if (existing.ConfirmedAt != null) throw new Exception("order is already paid!");

            var ticket = new Schema.Ticket
            {
                EventId = existing.EventId,
                Code = $"EVT-{existing.EventId}-ORD-{existing.Id}-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}"
            };

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            existing.TicketId = ticket.Id;
            existing.ConfirmedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void Cancel(int id)
        {
            var existing = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (existing == null) throw new Exception("could not find order!");
            if (existing.CanceledAt != null) throw new Exception("order is already canceled!");

            existing.CanceledAt = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public Event FindEventWithOrderById(int id)
        {
            var eventSchema = _context.Events.Include(e => e.Orders).FirstOrDefault(e => e.Id == id);
            if (eventSchema == null) throw new Exception("could not find event!");
            return new Event
            {
                Id = eventSchema.Id,
                Price = eventSchema.Price,
                Capacity = eventSchema.Capacity,
                Date = eventSchema.Date,
                Orders = eventSchema.Orders.Select(o => new Order
                {
                    Id = o.Id,
                    EventId = o.EventId,
                    TicketId = o.TicketId,
                    CreatedAt = o.CreatedAt,
                    ConfirmedAt = o.ConfirmedAt,
                    CanceledAt = o.CanceledAt
                }).ToList()
            };
        }

        public void Update(Order order)
        {
            var existing = _context.Orders.FirstOrDefault(o => o.Id == order.Id);
            if (existing == null) throw new Exception("could not find order!");

            existing.EventId = order.EventId;
            existing.TicketId = order.TicketId;
            existing.CreatedAt = order.CreatedAt;
            existing.ConfirmedAt = order.ConfirmedAt;
            existing.CanceledAt = order.CanceledAt;

            _context.SaveChanges();
        }

        public bool IsInDatabase(int id)
        {
            return _context.Orders.Any(o => o.Id == id);
        }
    }
}
