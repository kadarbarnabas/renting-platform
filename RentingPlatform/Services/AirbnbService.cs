using RentingPlatform.Shared;
using Microsoft.EntityFrameworkCore;

namespace RentingPlatform
{
    public class AirbnbService : IAirbnbService
    {
        private readonly RentingPlatformContext _context;
        private readonly ILogger<AirbnbService> _logger;

        public AirbnbService(ILogger<AirbnbService> logger, RentingPlatformContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task CreateAirbnb(Airbnbs airbnb)
        {
            await _context.Airbnb.AddAsync(airbnb);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Airbnb has been added");
        }

        public async Task<Airbnbs> GetAirbnb(Guid id)
        {
            var airbnb = await _context.Airbnb
                .Include(a => a.Ratings)
                .Include(a => a.Timetable)
                .FirstOrDefaultAsync(a => a.AirbnbId == id);

            _logger.LogInformation("Airbnb has been retrieved: {@Airbnb}", airbnb);
            return airbnb;
        }

        public async Task<List<Airbnbs>> GetAllAirbnbs()
        {
            _logger.LogInformation("All Airbnbs have been fetched");
            return await _context.Airbnb.ToListAsync();
        }

        public async Task DeleteAirbnb(Guid id)
        {
            var airbnb = await GetAirbnb(id);
            if (airbnb != null)
            {
                _context.Airbnb.Remove(airbnb);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Airbnb has been removed");
            }
            else
            {
                _logger.LogWarning("Airbnb not found");
            }
        }

        public async Task UpdateAirbnb(Airbnbs airbnb)
        {
            var existingAirbnb = await GetAirbnb(airbnb.AirbnbId);
            if (existingAirbnb != null)
            {
                existingAirbnb.Name = airbnb.Name;
                existingAirbnb.Description = airbnb.Description;
                existingAirbnb.PricePerNight = airbnb.PricePerNight;
                existingAirbnb.Location = airbnb.Location;
                existingAirbnb.Country = airbnb.Country; 
                existingAirbnb.Timetable = airbnb.Timetable;
                existingAirbnb.MaxGuests = airbnb.MaxGuests;
                existingAirbnb.Amenities = airbnb.Amenities;
                existingAirbnb.AverageRating = airbnb.AverageRating;
                existingAirbnb.Rooms = airbnb.Rooms; 
                existingAirbnb.Beds = airbnb.Beds; 
                existingAirbnb.Bathrooms = airbnb.Bathrooms; 
                _context.Airbnb.Update(existingAirbnb);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Airbnb has been updated: {@Airbnb}", existingAirbnb);
            }
            else
            {
                _logger.LogWarning("Airbnb to update not found");
            }
        }

        public async Task AddImageToAirbnb(Guid airbnbId, string imageUrl, Guid userId)
        {
            var airbnb = await GetAirbnb(airbnbId);
            if (airbnb != null)
            {
                airbnb.AddImage(imageUrl, userId);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Image has been added to Airbnb: {@AirbnbId}", airbnbId);
            }
            else
            {
                _logger.LogWarning("Airbnb not found");
            }
        }

        public async Task AddRatingToAirbnb(Guid airbnbId, int rating, Guid userId)
        {
            var airbnb = await GetAirbnb(airbnbId);
            if (airbnb != null)
            {
                airbnb.AddRating(rating, userId);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Rating has been added to Airbnb: {@AirbnbId}", airbnbId);
            }
            else
            {
                _logger.LogWarning("Airbnb not found");
            }
        }

        public async Task<DateTime?> GetNextAvailableDate(Guid airbnbId)
        {
            var airbnb = await GetAirbnb(airbnbId);
            if (airbnb != null)
            {
                var nextAvailableDate = airbnb.GetNextAvailableDate();
                _logger.LogInformation("Next available date for Airbnb {@AirbnbId}: {@NextAvailableDate}", airbnbId, nextAvailableDate);
                return nextAvailableDate;
            }
            else
            {
                _logger.LogWarning("Airbnb not found");
                return null;
            }
        }

        public async Task<decimal> CalculateTotalPrice(Guid airbnbId, DateTime startDate, DateTime endDate)
        {
            var airbnb = await GetAirbnb(airbnbId);
            if (airbnb != null)
            {
                var totalPrice = airbnb.CalculateTotalPrice(startDate, endDate);
                _logger.LogInformation("Total price for Airbnb {@AirbnbId}: {@TotalPrice}", airbnbId, totalPrice);
                return totalPrice;
            }
            else
            {
                _logger.LogWarning("Airbnb not found");
                return 0;
            }
        }
    }
}
