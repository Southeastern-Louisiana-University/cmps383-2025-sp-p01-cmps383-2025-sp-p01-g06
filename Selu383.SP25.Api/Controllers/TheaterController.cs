using System.Linq;
using System.Net;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet]
        public async Task<IActionResult> ListAllTheaters()
        {
            var theaters = await _context.Theaters
                .Select(getDto => new GetTheaterDto
                {
                    Id = new Guid(),
                    Name = getDto.Name,
                    Address = getDto.Address,
                    SeatCount = getDto.SeatCount,

                })
                .ToListAsync();

            return Ok(theaters);
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

            return CreatedAtAction(nameof(Create), new { id = newTheater.Id }, newTheater);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTheaterDto updateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Theater theater = await _context.Theaters.FindAsync(id);

            if (theater == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(updateDTO.Name))
            {
                return BadRequest("Theater name is required.");
            }

            if (updateDTO.Name.Length > 120)
            {
                return BadRequest("Theater name must be 120 characters or less.");
            }

            if (string.IsNullOrWhiteSpace(updateDTO.Address))
            {
                return BadRequest("Address is required.");
            }

            
            theater.Name = updateDTO.Name;
            theater.Address = updateDTO.Address;
            theater.SeatCount = updateDTO.SeatCount;

            await _context.SaveChangesAsync();

            return Ok(new { Theater = theater, Message = "Theater updated successfully!" });
        }
    }
}
