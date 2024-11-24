using RentingPlatform.Shared;

namespace RentingPlatform;

public interface IAirbnbService
{
    Task CreateAirbnb(Airbnb airbnb);
    Task DeleteAirbnb(Guid id);
    Task<Airbnb> GetAirbnb(Guid id);
    Task<List<Airbnb>> GetAllAirbnbs();
    Task UpdateAirbnb(Airbnb airbnb);
}
