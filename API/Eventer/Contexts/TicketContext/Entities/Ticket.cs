namespace Eventer.Contexts.TicketContext.Entities
{
    public class Ticket
    {
        public int Id { get; internal set; }
        public int EventId { get; private set; }
        public string Code { get; private set; } = "";

        private Ticket() { }

        public Ticket(int eventId)
        {
            if (eventId <= 0) throw new Exception("event id must be positive!");

            EventId = eventId;
            Code = GenerateCode();
        }

        internal Ticket(int id, int eventId, string code)
        {
            Id = id;
            EventId = eventId;
            Code = code;
        }

        private static string GenerateCode()
        {
            return Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        }
    }
}
