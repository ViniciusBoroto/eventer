using Eventer.Contexts.OrderContext.Interfaces;

namespace Eventer.Contexts.OrderContext.UseCases
{
    public class OrderDeleteCase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderDeleteCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Execute(int id)
        {
            try
            {
                if (_orderRepository.FindById(id) == null) throw new Exception("could not find order!");

                _orderRepository.Delete(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
