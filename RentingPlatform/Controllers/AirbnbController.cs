using RentingPlatform.Shared;
using Microsoft.AspNetCore.Mvc;

namespace RentingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

            if (existingAirbnb is not null)
            {
                return Conflict();
            }

            await _airbnbService.CreateAirbnb(airbnb);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAirbnb(Guid id)
        {
            var airbnb = await _airbnbService.GetAirbnb(id);

            if (airbnb is null)
            {
                return NotFound();
            }

            await _airbnbService.DeleteAirbnb(id);

            return Ok();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Airbnbs>> GetAirbnb(Guid id)
        {
            var airbnb = await _airbnbService.GetAirbnb(id);

            if (airbnb is null)
            {
                return NotFound();
            }

            return Ok(airbnb);
        }

        [HttpGet]
        public async Task<ActionResult<List<Airbnbs>>> GetAllAirbnbs()
        {
            return Ok(await _airbnbService.GetAllAirbnbs());
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAirbnb(Guid id, [FromBody] Airbnbs updatedAirbnb)
        {
            if (id != updatedAirbnb.AirbnbId)
            {
                return BadRequest();
            }

            var existingAirbnb = await _airbnbService.GetAirbnb(id);

            if (existingAirbnb is null)
            {
                return NotFound();
            }

            await _airbnbService.UpdateAirbnb(updatedAirbnb);

            return Ok();
        }

        [HttpPost("{id:guid}/images")]
        public async Task<IActionResult> AddImageToAirbnb(Guid id, [FromBody] string imageUrl, [FromQuery] Guid userId)
        {
            try
            {
                await _airbnbService.AddImageToAirbnb(id, imageUrl, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id:guid}/ratings")]
        public async Task<IActionResult> AddRatingToAirbnb(Guid id, [FromBody] int rating, [FromQuery] Guid userId)
        {
            try
            {
                await _airbnbService.AddRatingToAirbnb(id, rating, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
