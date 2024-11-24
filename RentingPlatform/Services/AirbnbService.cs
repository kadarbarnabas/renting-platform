using RentingPlatform.Shared;
using Microsoft.EntityFrameworkCore;

namespace RentingPlatform;

public class AirbnbService : IAirbnbService
{
    private readonly RentingPlatformContext _context;
    private readonly ILogger<AirbnbService> _logger;

    public AirbnbService(ILogger<AirbnbService> logger, RentingPlatformContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task CreateAirbnb(Airbnb airbnb)
    {
        await _context.Airbnb.AddAsync(airbnb);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Airbnb listing has been added");
    }

    public async Task<Airbnb> GetAirbnb(Guid id)
    {
        var airbnb = await _context.Airbnb.FindAsync(id);
        _logger.LogInformation("Airbnb listing retrieved: {@Airbnb}", airbnb);
        return airbnb;
    }

    public async Task<List<Airbnb>> GetAllAirbnbs()
    {
        _logger.LogInformation("All Airbnb listings fetched");
        return await _context.Airbnb.ToListAsync();
    }

    public async Task DeleteAirbnb(Guid id)
    {
        var airbnb = await GetAirbnb(id);
        _context.Airbnb.Remove(airbnb);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Airbnb listing deleted");
    }

    public async Task UpdateAirbnb(Airbnb updatedAirbnb)
    {
        var airbnb = await GetAirbnb(updatedAirbnb.AirbnbId);
        airbnb.Name = updatedAirbnb.Name;
        airbnb.Location = updatedAirbnb.Location;
        airbnb.PricePerNight = updatedAirbnb.PricePerNight;
        _logger.LogInformation("Airbnb listing updated: {@Airbnb}", airbnb);
        await _context.SaveChangesAsync();
    }
}
