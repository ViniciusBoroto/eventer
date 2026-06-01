using Eventer.Contexts.OrderContext.DTOs.Requests;
using Eventer.Contexts.OrderContext.Entities;
using Eventer.Contexts.OrderContext.Interfaces;
using Eventer.Contexts.TicketContext.Interfaces;
using Eventer.Contexts.ValueObject;

namespace Eventer.Contexts.OrderContext.UseCases
{
    public class OrderPayCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITicketRepository _ticketRepository;

        public OrderPayCase(IOrderRepository orderRepository, ITicketRepository ticketRepository)
        {
            _orderRepository = orderRepository;
            _ticketRepository = ticketRepository;
        }

        public void Execute(PayOrderRequest request)
        {
            try
            {
                Order order = _orderRepository.FindById(request.OrderId);

                new NullObject<Order>(order);

                Event orderEvent = _orderRepository.FindEventWithOrderById(request.EventId);

                new NullObject<Event>(orderEvent);

                Ticket t = new Ticket { EventId = request.EventId };
                _ticketRepository.Add(t);
                order.Pay();
                order.AddTicket(t.Id);

                _orderRepository.Update(order);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
