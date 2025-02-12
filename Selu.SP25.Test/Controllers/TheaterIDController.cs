using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Selu383.SP25.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TheatersController : ControllerBase
	{
		private static readonly List<Theater> theaters = new List<Theater>
		{
			new Theater { ID = 1, Name = "Cinema One", Address = "Downtown", SeatingCapacity = 200 },
			new Theater { ID = 2, Name = "Cinema Two", Address = "Uptown", SeatingCapacity = 300 }
		};

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var theater = theaters.FirstOrDefault(t => t.ID == id);

			if (theater == null)
			{
				return NotFound();
			}

			return Ok(theater);
		}
	}