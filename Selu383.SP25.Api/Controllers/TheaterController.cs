using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.Api.Entities;

namespace Selu383.SP25.Api.Controllers
{
    [ApiController]
    [Route("api/theaters")]
    public class TheaterController : ControllerBase
    {
        private readonly DataContext _context;

        public TheaterController(DataContext context)
        {
            _context = context;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTheaterDto updateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Theater theater = await _context.Theaters.FindAsync(id);

            if (theater == null)
            {
                return NotFound(new { error = "Theater not found." });
            }

            if (string.IsNullOrWhiteSpace(updateDTO.Name))
            {
                return BadRequest(new { error = "Theater name is required." });
            }

            if (updateDTO.Name.Length > 120)
            {
                return BadRequest(new { error = "Theater name must be 120 characters or less." });
            }

            if (string.IsNullOrWhiteSpace(updateDTO.Address))
            {
                return BadRequest(new { error = "Address is required." });
            }

            // Update the entity with new values
            theater.Name = updateDTO.Name;
            theater.Address = updateDTO.Address;
            theater.SeatCount = updateDTO.SeatCount;

            await _context.SaveChangesAsync();

            // 🔹 Convert updated entity to DTO
            var updatedTheaterDto = new GetTheaterDto
            {
                Id = theater.Id,
                Name = theater.Name,
                Address = theater.Address,
                SeatCount = theater.SeatCount
            };

            return Ok(updatedTheaterDto); // 🔹 Return DTO instead of empty response
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);

            if (theater == null)
            {
                return NotFound();
            }

            _context.Theaters.Remove(theater);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTheaterDto createDTO)
        {
            if (createDTO.SeatCount < 1)
            {
                return BadRequest(new { error = "Theatre must have at least 1 seat." });
            }

            Theater newTheater = new Theater
            {
                Name = createDTO.Name,
                Address = createDTO.Address,
                SeatCount = createDTO.SeatCount,
            };

            _context.Add(newTheater);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTheaterById), new { id = newTheater.Id }, newTheater);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTheaterById(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);

            if (theater == null)
            {
                return NotFound(new { error = "Theater not found." });
            }

            // Convert entity to DTO
            var theaterDto = new GetTheaterByIdDto
            {
                Id = theater.Id,
                Name = theater.Name,
                SeatCount = theater.SeatCount,
                Address = theater.Address
            };

            return Ok(theaterDto);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Theater>>> GetAllTheaters()
        {
            var theaters = await _context
                .Theaters.Select(t => new GetTheaterDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Address = t.Address,
                    SeatCount = t.SeatCount,
                })
                .ToListAsync();

           

            return Ok(theaters);
        }
    }
}
