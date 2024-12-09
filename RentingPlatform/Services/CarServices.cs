using RentingPlatform.Shared;
using Microsoft.EntityFrameworkCore;

namespace RentingPlatform;

public class CarService : ICarService
{
    private readonly RentingPlatformContext _context;
    private readonly ILogger<CarService> _logger;

    public CarService(ILogger<CarService> logger, RentingPlatformContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Create(Car Car)
    {
        await _context.Cars.AddAsync(Car);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Car has been added");
    }

    public async Task<Car> Get(Guid id)
    {
        var car = await _context.Cars.Include(x => x.Owner).FirstOrDefaultAsync(x => x.CarId == id);
        _logger.LogInformation("Car has been retrieved: {@Car}", car);
        return car;
    }

    public async Task Delete(Guid id)
    {
        var car = await Get(id);
        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Car has been removed");
    }

    public async Task<List<Car>> GetAll()
    {
        _logger.LogInformation("Every car has been fetched");
        return await _context.Cars.ToListAsync();
    }

    public async Task Update(Car updatedCar)
    {
        var car = await Get(updatedCar.CarId);
        car.Brand = updatedCar.Brand;
        car.Model = updatedCar.Model;
        car.PlateNumber = updatedCar.PlateNumber;
        car.Description = updatedCar.Description;
        car.Image = updatedCar.Image;
        car.Price = updatedCar.Price;
        car.Location = updatedCar.Location;
        await _context.SaveChangesAsync();
        _logger.LogInformation("{Car} has been updated", car);
    }

    public async Task<List<string>> GetBrandsAsync()
    {
        return await _context.Cars
            .Select(c => c.Brand)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Car>> GetFilteredCarsAsync(string? brand, int? rating)
    {
        var query = _context.Cars.AsQueryable();

        if (!string.IsNullOrEmpty(brand))
        {
            query = query.Where(c => c.Brand == brand);
        }

        if (rating.HasValue)
        {
            query = query.Where(c => c.AverageRating >= rating.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<bool> BookCarAsync(Guid carId, DateTime startDate, DateTime endDate, Guid userId)
        {
            var booking = new CarBookings
            {
                BookingId = Guid.NewGuid(),
                CarId = carId,
                StartDate = startDate,
                EndDate = endDate,
                UserId = userId,
            };

            _context.CarBookings.Add(booking);
            await _context.SaveChangesAsync();

            return true;
        }

    public async Task<bool> GetBookings(Guid carId, DateTime startDate, DateTime endDate, Guid userId)
    {
        try
        {
            bool isBooked = await _context.CarBookings
                .AnyAsync(b =>
                    b.CarId == carId &&
                    b.StartDate < endDate &&
                    b.EndDate > startDate);

            return isBooked; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking booking: {ex.Message}");
            return false;
        }
    }

    public async Task<List<BookingPeriod>> GetBookingsForCarAsync(Guid carId)
    {
        try
    {
        return await _context.CarBookings
            .Where(b => b.CarId == carId)
            .Select(b => new BookingPeriod
            {
                StartDate = b.StartDate ?? DateTime.MinValue,
                EndDate = b.EndDate ?? DateTime.MinValue
            })
            .ToListAsync();
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error fetching bookings for car: {ex.Message}");
        return new List<BookingPeriod>();
    }
    }

    public class BookingPeriod
    {
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    }

    public async Task<bool> DeleteBookingAsync(Guid bookingId)
    {
    try
    {
        // Find the booking by ID
        var booking = await _context.CarBookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
        

        // Remove the booking
        _context.CarBookings.Remove(booking);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Booking with ID {BookingId} deleted successfully.", bookingId);
        return true;
    }
    catch (Exception ex)
    {
        _logger.LogError("Error deleting booking: {Message}", ex.Message);
        return false;
    }
    }

}