using Eventer.Contexts.OrderContext.Entities;

namespace Eventer.Contexts.OrderContext.Interfaces
{
    public interface IOrderRepository
    {
        public Order FindById(int id);
        public List<Order> GetAll();
        public void Add(Order orderEntity);
        public void Delete(int id);
        public Event FindWithOrdersById(int id);
        public void Update(Order order);
    }
}
