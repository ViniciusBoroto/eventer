using Eventer.Contexts.OrderContext.Interfaces;

namespace Eventer.Contexts.OrderContext.UseCases
{
    public class OrderPayCase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderPayCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Execute(int orderId)
        {
            try
            {
                if (!_orderRepository.IsInDatabase(orderId)) throw new Exception("could not find order!");

                _orderRepository.Pay(orderId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
