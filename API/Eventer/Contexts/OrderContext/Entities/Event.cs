using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventer.Contexts.OrderContext.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime Date { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

        public bool IsAvailable()
        {
            return Orders.Count(o => !o.IsCanceled()) < Capacity;
        }
    }


}