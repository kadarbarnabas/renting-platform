using RentingPlatform.Shared;
using Microsoft.AspNetCore.Mvc;

namespace RentingPlatform.Controllers;

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
    public async Task<IActionResult> CreateAirbnb([FromBody] Airbnb airbnb)
    {
        var existingAirbnb = await _airbnbService.GetAirbnb(airbnb.AirbnbId);
        if (existingAirbnb is not null)
        {
            return Conflict();
        }
        await _airbnbService.CreateAirbnb(airbnb);
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Airbnb>> GetAirbnb(Guid id)
    {
        var airbnb = await _airbnbService.GetAirbnb(id);
        if (airbnb is null)
        {
            return NotFound();
        }
        return Ok(airbnb);
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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAirbnb(Guid id, [FromBody] Airbnb updatedAirbnb)
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

    [HttpGet]
    public async Task<ActionResult<List<Airbnb>>> GetAllAirbnbs()
    {
        return Ok(await _airbnbService.GetAllAirbnbs());
    }
}
