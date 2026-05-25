using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventer.Database.Schemas
{
    public class Order
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CanceledAt { get; set; }
        public Ticket? Ticket { get; set; }
        public int? TicketId { get; set; }
    }
}