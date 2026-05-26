namespace Eventer.Database.Schemas
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string Code { get; set; }
        public Order Order { get; set; }
    }
}