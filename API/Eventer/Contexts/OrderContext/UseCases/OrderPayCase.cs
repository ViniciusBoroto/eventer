using Eventer.Contexts.OrderContext.DTOs.Requests;
using Eventer.Contexts.OrderContext.Entities;
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

        public void Execute(PayOrderRequest request)
        {
            Order order = _orderRepository.FindById(request.OrderId);

            if (order == null) throw new Exception("unknown order!");

            Event eventToUse = _orderRepository.FindWithOrdersById(request.EventId);

            if (eventToUse == null) throw new Exception("unknown ticket");

            Ticket t = new Ticket
            {
                Code = "7f851",
                Event = eventToUse,
                Order = order
            };

            order.ConfirmedAt = DateTime.Now;
            _orderRepository.Update(order);
        }
    }
}
