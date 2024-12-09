using RentingPlatform.Shared;
using static RentingPlatform.CarService;

namespace RentingPlatform;

public interface ICarService{

    Task Create(Car car);
    Task Delete(Guid id);
    Task<Car> Get(Guid id);
    Task<List<Car>> GetAll();
    Task Update(Car car);
    Task<List<string>> GetBrandsAsync();
    Task<List<Car>> GetFilteredCarsAsync(string? brand, int? rating);
    Task<bool> BookCarAsync(Guid carId, DateTime startDate, DateTime endDate, Guid userId);
    Task<bool> GetBookings(Guid carId, DateTime startDate, DateTime endDate, Guid userId);
    Task<List<BookingPeriod>> GetBookingsForCarAsync(Guid carId);
    Task<bool> DeleteBookingAsync(Guid bookingId);

}