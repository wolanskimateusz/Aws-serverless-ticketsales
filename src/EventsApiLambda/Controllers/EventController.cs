using api.Data;
using api.Dtos.Artist;
using api.Dtos.Event;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepo;
        private readonly IArtistRepository _artistRepo;
        public EventController(IEventRepository eventRepo, IArtistRepository artistRepo)
        {
            _eventRepo = eventRepo;
            _artistRepo = artistRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _eventRepo.GetAllAsync(query);
           
            var eventDto = result.Select(x => x.ToEventDto()).ToList();

            return Ok(eventDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _eventRepo.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result.ToEventDto());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventDto eventDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            List<Artist> ArtistList = new List<Artist>();

            foreach(string element in eventDto.Artists)
            {
                Artist artist = await _artistRepo.GetArtistByNameAsync(element);
                if (artist == null) continue;
                ArtistList.Add(artist);
            }

            var eventModel = eventDto.ToEventFromCreateDto();
            eventModel.Artists = ArtistList;

            await _eventRepo.CreateAsync(eventModel);

            return CreatedAtAction(nameof(GetById), new { id = eventModel.Id }, eventModel.ToEventDto());
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEventDto eventDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _eventRepo.UpdateAsync(id, eventDto);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _eventRepo.DeleteAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _eventRepo.GetLatestAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("artist")]
        public async Task<IActionResult> AddArtist(int artistId, int eventId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _eventRepo.AddArtistToEventAsync(eventId, artistId);

            if (result == null) return BadRequest(result);

            return Ok(result);
        }


        [HttpPut("artistnew")]
        public async Task<IActionResult> AddNewArtist([FromBody] CreateArtistDto artist, int eventId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newArtist = await _artistRepo.CreateAsync(artist.ToArtistFromCreateDto());

            if (newArtist == null) return BadRequest(newArtist);

            var result = await _eventRepo.AddArtistToEventAsync(eventId, newArtist.Id);

            if (result == null) return BadRequest(result);

            return Ok(result);
        }


        [HttpDelete("artist")]
        public async Task<IActionResult> DeleteArtist(int eventId, int artistId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _eventRepo.DeleteArtistFromEventAsync(eventId, artistId);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("eventList")]
        public async Task<IActionResult> GetArtistList(int artistId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _eventRepo.GetEventsWtihArtistByIdAsync(artistId);
            if (result == null) return NotFound();

            return Ok(result);
        }

    }

    
}
