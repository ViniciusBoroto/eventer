namespace Eventer.Database.Schemas
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
        public string Code { get; set; } = "";
        public Order Order { get; set; } = null!;
    }
}
