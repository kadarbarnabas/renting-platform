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

    [HttpGet("brands")]
    public async Task<ActionResult<List<string>>> GetBrands()
    {
        var brands = await _CarService.GetBrandsAsync();
        return Ok(brands);
    }

    [HttpGet("filter")]
    public async Task<ActionResult<List<Car>>> GetFilteredCars(string? brand, int? rating)
    {
        var cars = await _CarService.GetFilteredCarsAsync(brand, rating);
        return Ok(cars);
    }

    [HttpPost("book")]
    public async Task<IActionResult> BookCar([FromBody] BookingRequest request)
        {
        try
    {
        if (request.StartDate >= request.EndDate)
            return BadRequest("Invalid date range.");

        var success = await _CarService.BookCarAsync(request.CarId, request.StartDate, request.EndDate, request.UserId);

        if (success)
            return Ok(new { message = "Booking successful" });
        else
            return StatusCode(500, new { message = "Something went wrong." });
    }
        catch (Exception ex)
    {
        Console.WriteLine($"Error while booking car: {ex.Message}");
        return StatusCode(500, new { message = "Internal server error" });
    }
        }

        public class BookingRequest
    {
        public Guid CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }
    }

    [HttpPost("check-booking")]
public async Task<IActionResult> CheckCarBooking([FromBody] BookingRequest request)
{
    try
    {
        var isBooked = await _CarService.GetBookings(request.CarId, request.StartDate, request.EndDate, request.UserId);

        if (isBooked)
        {
            return Ok(new { message = "Car is already booked for the selected dates." });
        }
        else
        {
            return Ok(new { message = "Car is available for booking." });
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during booking check: {ex.Message}");
        return StatusCode(500, new { message = "Internal server error" });
    }
}

[HttpPost("check-bookings")]
public async Task<IActionResult> GetBookingsForCar([FromBody] BookingCheckRequest request)
{
    try
    {
        var bookings = await _CarService.GetBookingsForCarAsync(request.CarId);
        
        // Visszaadjuk a foglalások időszakait
        return Ok(bookings);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error fetching bookings: {ex.Message}");
        return StatusCode(500, new { message = "Internal server error" });
    }
}

public class BookingCheckRequest
{
    public Guid CarId { get; set; }
}

[HttpDelete("delete-booking/{bookingId:guid}")]
public async Task<IActionResult> DeleteBooking(Guid bookingId)
{
    try
    {
        var success = await _CarService.DeleteBookingAsync(bookingId);

        if (success)
        {
            return Ok(new { message = "Booking deleted successfully." });
        }
        else
        {
            return NotFound(new { message = "Booking not found." });
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error deleting booking: {ex.Message}");
        return StatusCode(500, new { message = "Internal server error." });
    }
}

}
