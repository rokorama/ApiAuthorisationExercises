using CarAPI.Models;

namespace CarAPI.Services;

public interface ICarRepository
{
    public CarDto SaveCar(CarDto carDto);
    public bool DeleteCar(Guid id);
    public IEnumerable<Car> GetCar();
    public Car GetCar(Guid id);
    public CarDto PutCar(Guid id, CarDto carDto);

}