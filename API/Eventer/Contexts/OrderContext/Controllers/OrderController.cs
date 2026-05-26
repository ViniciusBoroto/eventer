using Eventer.Contexts.OrderContext.DTOs.Requests;
using Eventer.Contexts.OrderContext.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Eventer.Contexts.OrderContext.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderDeleteCase _deleteCase;
        private readonly OrderCreateCase _createCase;
        private readonly OrderGetAllCase _getAllCase;
        private readonly OrderGetByIdCase _getByIdCase;
        private readonly OrderPayCase _payCase;
        private readonly OrderCancelCase _cancelCase;

        public OrderController(
            OrderDeleteCase deleteCase,
            OrderCreateCase createCase,
            OrderGetAllCase getAllCase,
            OrderGetByIdCase getByIdCase,
            OrderPayCase payCase,
            OrderCancelCase cancelCase)
        {
            _deleteCase = deleteCase;
            _createCase = createCase;
            _getAllCase = getAllCase;
            _getByIdCase = getByIdCase;
            _payCase = payCase;
            _cancelCase = cancelCase;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try { return Ok(_getAllCase.Execute()); }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try { return Ok(_getByIdCase.Execute(id)); }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderRequest request)
        {
            try { _createCase.Execute(request); }
            catch (Exception e) { return BadRequest(e.Message); }

            return Ok();
        }

        [HttpPost("{payRequest.OrderId}/pay")]
        public IActionResult Pay(PayOrderRequest payRequest)
        {
            try { _payCase.Execute(payRequest); }
            catch (Exception e) { return BadRequest(e.Message); }

            return Ok();
        }

        [HttpPost("{id}/cancel")]
        public IActionResult Cancel(int id)
        {
            try { _cancelCase.Execute(id); }
            catch (Exception e) { return BadRequest(e.Message); }

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try { _deleteCase.Execute(id); }
            catch (Exception e) { return BadRequest(e.Message); }

            return Ok();
        }
    }
}
