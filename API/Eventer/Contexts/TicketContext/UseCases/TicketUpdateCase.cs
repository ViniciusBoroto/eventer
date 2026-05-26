using Eventer.Contexts.TicketContext.Entities;
using Eventer.Database.Schemas;

namespace Eventer.Contexts.TicketContext.UseCases
{
    public class TicketUpdateCase
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public string Code { get; set; } = string.Empty;
        public Order Order { get; set; }
    }
}
