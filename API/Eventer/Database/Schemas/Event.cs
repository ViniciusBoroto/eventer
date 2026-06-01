namespace Eventer.Database.Schemas
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string PictureUrl { get; set; } = "";
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; } = "";
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}