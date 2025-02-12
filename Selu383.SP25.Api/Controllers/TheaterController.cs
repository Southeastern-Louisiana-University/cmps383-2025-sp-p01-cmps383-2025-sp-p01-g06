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

            return Ok(theater);
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

            if (!theaters.Any())
            {
                return NoContent();
            }

            return Ok(theaters);
        }
    }
}
