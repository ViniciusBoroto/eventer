using Eventer.Contexts.OrderContext.DTOs.Requests;
using Eventer.Contexts.OrderContext.Entities;
using Eventer.Contexts.OrderContext.Interfaces;
using Eventer.Contexts.TicketContext.Entities;
using Eventer.Contexts.TicketContext.Interfaces;

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
                if (order == null)
                {
                    throw new Exception("Order not found");
                }


                Event orderEvent = _orderRepository.FindEventWithOrderById(request.EventId);

                if (orderEvent == null)
                {
                    throw new Exception("Event not found");
                }

                Ticket t = new Ticket(request.EventId);
                _ticketRepository.Create(t);
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
