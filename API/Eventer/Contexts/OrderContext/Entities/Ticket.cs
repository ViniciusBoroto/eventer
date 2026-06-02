using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventer.Contexts.OrderContext.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Code { get; set; }
        public Ticket(int eventId)
        {
            EventId = eventId;
            Code = Guid.NewGuid().ToString().Substring(0, 5);
        }
    }
}