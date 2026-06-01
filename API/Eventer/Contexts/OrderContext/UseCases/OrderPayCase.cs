using Eventer.Contexts.OrderContext.DTOs.Requests;
using Eventer.Contexts.OrderContext.Entities;
using Eventer.Contexts.OrderContext.Interfaces;
using Eventer.Contexts.ValueObject;

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
            try
            {
                Order order = _orderRepository.FindById(request.OrderId);

                new NullObject<Order>(order);

                Event orderEvent = _orderRepository.FindEventWithOrderById(request.EventId);

                new NullObject<Event>(orderEvent);

                Ticket t = new Ticket
                {
                    Code = "7f851",
                    Event = orderEvent,
                    Order = order
                };

                order.ConfirmedAt = DateTime.Now;
                _orderRepository.Update(t.Order);
                _orderRepository.Pay(t.Order.Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
