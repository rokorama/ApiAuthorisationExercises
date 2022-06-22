using CarAPI.Models;
using CarAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CarAPI.Services;

public class CarRepository : ICarRepository
{
    private readonly ApplicationDbContext _context;

    public CarRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public CarDto SaveCar(CarDto carDto)
    {
        var entry = new Car
        {
            Id = Guid.NewGuid(),
            Model = carDto.Model,
            Colour = carDto.Colour
        };
        _context.Cars.Add(entry);
        if (_context.SaveChanges() != 0)
            return carDto;
        return null;
        // return _context.Cars;
    }

    public bool DeleteCar(Guid id)
    {
        var result = _context.Cars.Find(id);
        _context.Cars.Remove(result);
        if (_context.SaveChanges() != 0)
            return true;
        return false;
    }

    public IEnumerable<Car> GetCar()
    {
        return _context.Cars;
    }

    public Car GetCar(Guid id)
    {
        var result = _context.Cars.Find(id);
        return result;
    }

    // doesn't work
    public CarDto PutCar(Guid id, CarDto carDto)
    {
        var dbEntry = _context.Cars.SingleOrDefault(a => a.Id == id);
        dbEntry.Model = carDto.Model;
        dbEntry.Colour = carDto.Colour;

        _context.Entry(dbEntry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        
        try
        {
            _context.SaveChanges();
            return carDto;
        }
        catch (DbUpdateConcurrencyException)
        {
            return null;
        }
    }

    private bool CarExists(Guid id)
    {
        return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}