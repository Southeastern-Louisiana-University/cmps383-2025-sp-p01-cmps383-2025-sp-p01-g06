using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("CreateTheater")]
        public async Task<IActionResult> Create(CreateTheaterDto createDTO)
        {
            if (createDTO.SeatCount < 1)
            {
                return BadRequest(new { error = "Theatre must have at least 1 seat." });
            }

            Theater newTheater = new Theater
            {
                Id = Guid.NewGuid(),
                Name = createDTO.Name,
                Address = createDTO.Address,
                SeatCount = createDTO.SeatCount,
            };

            _context.Add(newTheater);

            await _context.SaveChangesAsync();

            return Ok(createDTO);
        }
    }
}
