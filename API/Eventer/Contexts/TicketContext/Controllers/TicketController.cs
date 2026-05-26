using Eventer.Contexts.TicketContext.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eventer.Contexts.TicketContext.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return _ticketRepository.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return _ticketRepository.FindById(id);
        }
    }
}
