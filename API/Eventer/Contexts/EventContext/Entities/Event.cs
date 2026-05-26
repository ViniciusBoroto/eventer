namespace Eventer.Contexts.EventContext.Entities
{
    public class Event
    {
        public int Id { get; internal set; }
        public string Name { get; private set; } = "";
        public string Description { get; private set; } = "";
        public string PictureUrl { get; private set; } = "";
        public decimal Price { get; private set; }
        public int Capacity { get; private set; }
        public DateTime Date { get; private set; }
        public string Location { get; private set; } = "";

        private Event() { }

        public Event(string name, string description, string pictureUrl, decimal price, int capacity, DateTime date, string location)
        {
            if (price < 0) throw new Exception("price cannot be negative!");
            if (capacity <= 0) throw new Exception("capacity must be positive");

            Name = name;
            Description = description;
            PictureUrl = pictureUrl;
            Price = price;
            Capacity = capacity;
            Date = date;
            Location = location;
        }

        internal Event(int id, string name, string description, string pictureUrl, decimal price, int capacity, DateTime date, string location)
            : this(name, description, pictureUrl, price, capacity, date, location)
        {
            Id = id;
        }

        public void Update(string name, string description, string pictureUrl, decimal price, int capacity, DateTime date, string location)
        {
            if (price < 0) throw new Exception("price cannot be negative!");
            if (capacity <= 0) throw new Exception("capacity must be positive");

            Name = name;
            Description = description;
            PictureUrl = pictureUrl;
            Price = price;
            Capacity = capacity;
            Date = date;
            Location = location;
        }
    }
}
