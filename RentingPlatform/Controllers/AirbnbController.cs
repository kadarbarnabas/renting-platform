using RentingPlatform.Shared;
using Microsoft.AspNetCore.Mvc;

namespace RentingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]/asd")]
    public class AirbnbController : ControllerBase
    {
        private readonly IAirbnbService _airbnbService;

        public AirbnbController(IAirbnbService airbnbService)
        {
            _airbnbService = airbnbService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAirbnb([FromBody] Airbnbs airbnb)
        {
            var existingAirbnb = await _airbnbService.GetAirbnb(airbnb.AirbnbId);

            if (existingAirbnb != null)
            {
                return Conflict("An Airbnb with the same ID already exists.");
            }

            await _airbnbService.CreateAirbnb(airbnb);
            return CreatedAtAction(nameof(GetAirbnb), new { id = airbnb.AirbnbId }, airbnb);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAirbnb(Guid id)
        {
            var airbnb = await _airbnbService.GetAirbnb(id);

            if (airbnb == null)
            {
                return NotFound($"Airbnb with ID {id} not found.");
            }

            await _airbnbService.DeleteAirbnb(id);
            return NoContent();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Airbnbs>> GetAirbnb(Guid id)
        {
            var airbnb = await _airbnbService.GetAirbnb(id);

            if (airbnb == null)
            {
                return NotFound($"Airbnb with ID {id} not found.");
            }

            return Ok(airbnb);
        }

        [HttpGet]
        public async Task<ActionResult<List<Airbnbs>>> GetAllAirbnbs()
        {
            var airbnbs = await _airbnbService.GetAllAirbnbs();
            return Ok(airbnbs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAirbnb(Guid id, [FromBody] Airbnbs updatedAirbnb)
        {
            if (id != updatedAirbnb.AirbnbId)
            {
                return BadRequest("ID in the URL does not match ID in the request body.");
            }

            var existingAirbnb = await _airbnbService.GetAirbnb(id);

            if (existingAirbnb == null)
            {
                return NotFound($"Airbnb with ID {id} not found.");
            }

            await _airbnbService.UpdateAirbnb(updatedAirbnb);
            return Ok("Airbnb has been successfully updated.");
        }

        [HttpPost("{id:guid}/images")]
        public async Task<IActionResult> AddImageToAirbnb(Guid id, [FromBody] string imageUrl, [FromQuery] Guid userId)
        {
            try
            {
                await _airbnbService.AddImageToAirbnb(id, imageUrl, userId);
                return Ok("Image has been successfully added.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding image: {ex.Message}");
            }
        }

        [HttpPost("{id:guid}/ratings")]
        public async Task<IActionResult> AddRatingToAirbnb(Guid id, [FromBody] int rating, [FromQuery] Guid userId)
        {
            if (rating < 1 || rating > 5)
            {
                return BadRequest("Rating must be between 1 and 5.");
            }

            try
            {
                await _airbnbService.AddRatingToAirbnb(id, rating, userId);
                return Ok("Rating has been successfully added.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding rating: {ex.Message}");
            }
        }
    }
}
