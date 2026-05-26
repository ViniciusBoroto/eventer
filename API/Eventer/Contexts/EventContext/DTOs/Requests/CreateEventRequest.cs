using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventer.Contexts.EventContext.DTOs.Requests
{
    public class CreateEventRequest
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string PictureUrl { get; set; } = "";
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; } = "";
    }

}
