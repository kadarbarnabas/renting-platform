using RentingPlatform.Shared;
using Microsoft.AspNetCore.Mvc;

namespace RentingPlatform.Controllers;

[ApiController]
[Route("[controller]/route_car")]
public class CarController : ControllerBase
{
    private readonly ICarService _CarService;

    public CarController(ICarService CarService)
    {
        _CarService = CarService;
    }
    [HttpPost("{id:guid}/upload-image")]
public async Task<IActionResult> UploadImage(Guid id, IFormFile imageFile)
{
    if (imageFile == null || imageFile.Length == 0)
    {
        return BadRequest("No file uploaded.");
    }

    var car = await _CarService.Get(id);
    if (car == null)
    {
        return NotFound("Car not found.");
    }

    using (var memoryStream = new MemoryStream())
    {
        await imageFile.CopyToAsync(memoryStream);
        car.Image = memoryStream.ToArray();
    }

    await _CarService.Update(car);

    return Ok("Image uploaded successfully.");
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
        return Ok(Car.CarId);
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
