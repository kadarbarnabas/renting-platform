using RentingPlatform.Shared;

namespace RentingPlatform;

public interface ICarService{

    Task Create(Car car);
    Task Delete(Guid id);
    Task<Car> Get(Guid id);
    Task<List<Car>> GetAll();
    Task Update(Car car);

}