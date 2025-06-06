using api.Dtos.Artist;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    [ApiController]
    [Route("api/artist")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepo;
        public ArtistController(IArtistRepository aristRepo)
        {
            _artistRepo = aristRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _artistRepo.GetAllAsync();

            var artistDto = result.Select(x => x.ToArtistDto()).Distinct().ToList();

            return Ok(artistDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _artistRepo.GetByIdAsync(id);
            if(result == null) return NotFound();

            return Ok(result.ToArtistDto());  
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtistDto artistDto)
        {
            if(!ModelState.IsValid)  return BadRequest(ModelState); 
            
            var artistModel = artistDto.ToArtistFromCreateDto();
            var result = await _artistRepo.GetArtistByNameAsync(artistModel.Name);
            if(result == null)
            {
                await _artistRepo.CreateAsync(artistModel);
                return CreatedAtAction(nameof(GetById), new { id = artistModel.Id }, artistModel.ToArtistDto());
            }
            return BadRequest("Taki artysta już istnieje");
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateArtistDto aristModel)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _artistRepo.UpdateAsync(id, aristModel);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _artistRepo.DeleteAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _artistRepo.GetGenresAsync();
            if (result == null) return NotFound();

            return Ok(result);
        }
        [HttpGet("genre/{genre}")]
        public async Task<IActionResult> GetArtistByGenre([FromRoute] string genre)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _artistRepo.GetArtistsByGenreAsync(genre);
            if(result == null) return NotFound();
            return Ok(result);
        }
    }

}
