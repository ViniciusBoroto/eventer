using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventer.Contexts.EventContext.DTOs.Requests;
using Eventer.Contexts.EventContext.UseCases;
using Eventer.Database.Schemas;
using Microsoft.AspNetCore.Mvc;

namespace Eventer.Contexts.EventContext.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventUpdateCase _updateCase;
        private readonly EventDeleteCase _deleteCase;
        private readonly EventCreateCase _createCase;
        private readonly EventGetAllCase _getAllCase;
        private readonly EventGetByIdCase _getByIdCase;

        public EventController(
            EventUpdateCase updateCase,
            EventDeleteCase deleteCase,
            EventCreateCase createCase,
            EventGetAllCase getAllCase,
            EventGetByIdCase getByIdCase
        ) {
            _updateCase = updateCase;
            _deleteCase = deleteCase;
            _createCase = createCase;
            _getAllCase = getAllCase;
            _getByIdCase = getByIdCase;
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
        public IActionResult Post([FromBody] CreateEventRequest request)
        {
            try { _createCase.Execute(request); }
            catch (Exception e) { return BadRequest(e.Message); }

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateEventRequest request)
        {
            try { _updateCase.Execute(request); }
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