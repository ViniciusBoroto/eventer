namespace Eventer.Contexts.OrderContext.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int? TicketId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CanceledAt { get; set; }

        public Order() { }
        public Order(int Id, int eventId)
        {
            this.Id = Id;
            this.EventId = eventId;
            CreatedAt = DateTime.UtcNow;
        }

        public bool IsCanceled()
        {
            return CanceledAt != null;
        }
    }


}
