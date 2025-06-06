using api.Data;
using api.Dtos.Ticket;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace api.Controllers
{
    [ApiController]
    [Route("api/ticket")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepo;
        private readonly IEventRepository _eventRepo;
        private readonly IUserRepository _userRepo;


        public TicketController(ITicketRepository ticketRepo, IEventRepository eventRepo, IUserRepository userRepo)
        {
            _ticketRepo = ticketRepo;
            _eventRepo = eventRepo; 
            _userRepo = userRepo;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           if (!ModelState.IsValid) return BadRequest(ModelState);
            var results = await _ticketRepo.GetAllAsync();
           if (results.Count == 0) return NotFound();

           var ticketDto = results.Select(x => x.ToTicketDto()).ToList();
           return Ok(ticketDto);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
           if (!ModelState.IsValid) return BadRequest(ModelState);
           var result = await _ticketRepo.GetByIdAsync(id);
           if(result == null) return NotFound();
           var evresult = await _eventRepo.GetByIdAsync(result.EventId);
            result.Event = evresult;
           return Ok(result.ToTicketDto());
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateTicketDto ticketDto)
        {
           if(!ModelState.IsValid) return BadRequest(ModelState);

           var result = await _eventRepo.GetByIdAsync(ticketDto.EventId);
            if (result == null) return BadRequest("Event does not exist");

            var userResult = await _userRepo.GetByNameAsync(ticketDto.Owner);
            if (userResult == null) return BadRequest("User not found");

           var ticketModel = ticketDto.ToTicketFromCreateDto();
            ticketModel.Event = result;
            ticketModel.Owner = userResult;
           await _ticketRepo.CreateAsync(ticketModel);

            return CreatedAtAction(nameof(GetById), new { id = ticketModel.Id }, ticketModel.ToTicketDto());

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTicketDto ticketDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _ticketRepo.UpdateAsync(id,ticketDto);
            if (result == null) return NotFound();

            return Ok(result);


        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _ticketRepo.DeleteAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
