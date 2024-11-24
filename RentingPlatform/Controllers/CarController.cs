using RentingPlatform.Shared;
using Microsoft.AspNetCore.Mvc;

namespace RentingPlatform.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _CarService;

    public CarController(ICarService CarService)
    {
        _CarService = CarService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Car Car)
    {
        var existingCar = await _CarService.Get(Car.CarId);
        if (existingCar is not null)
        {
            return Conflict();
        }
        await _CarService.Create(Car);
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Car>> Get(Guid id)
    {
        var Car = await _CarService.Get(id);
        if (Car is null)
        {
            return NotFound();
        }
        return Ok(Car);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var Car = await _CarService.Get(id);
        if (Car is null)
        {
            return NotFound();
        }
        await _CarService.Delete(id);
        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Car updatedCar)
    {
        if (id != updatedCar.CarId)
        {
            return BadRequest();
        }
        var existingCar = await _CarService.Get(id);
        if (existingCar is null)
        {
            return NotFound();
        }
        await _CarService.Update(updatedCar);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<Car>>> GetAll()
    {
        return Ok(await _CarService.GetAll());
    }
}
