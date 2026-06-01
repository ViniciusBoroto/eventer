namespace Eventer.Contexts.EventContext.Entities
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

        public void Update(string name, string description, string pictureUrl, decimal price, int capacity, DateTime date, string location)
        {
            this.Name = name;
            this.Description = description;
            this.PictureUrl = pictureUrl;
            this.Price = price;
            this.Capacity = capacity;
            this.Date = date;
            this.Location = location;
        }
    }
}
