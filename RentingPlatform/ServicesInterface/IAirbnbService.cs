using RentingPlatform.Shared;

namespace RentingPlatform
{
    public interface IAirbnbService
    {
        Task CreateAirbnb(Airbnbs airbnb);
        Task DeleteAirbnb(Guid id);
        Task<Airbnbs> GetAirbnb(Guid id);
        Task<List<Airbnbs>> GetAllAirbnbs();
        Task UpdateAirbnb(Airbnbs airbnb);
        Task AddImageToAirbnb(Guid airbnbId, string imageUrl, Guid userId);
        Task AddRatingToAirbnb(Guid airbnbId, int rating, Guid userId);
    }
}
