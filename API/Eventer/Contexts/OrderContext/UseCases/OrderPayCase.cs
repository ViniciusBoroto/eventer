using Eventer.Contexts.OrderContext.DTOs.Requests;
using Eventer.Contexts.OrderContext.Entities;
using Eventer.Contexts.OrderContext.Interfaces;

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
            Order order = _orderRepository.FindById(request.OrderId);

            if (order == null) throw new Exception("unknown order!");

            Ticket ticket = _ticketRepository.FindById(request.TicketId);

            if (ticket == null) throw new Exception("unknown ticket");

            order.TicketId = ticket.Id;
            order.ConfirmedAt = DateTime.Now;
            _orderRepository.Update(order);
        }
    }
}
