using Microsoft.AspNetCore.Mvc;
using CarAPI.Models;
using CarAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CarAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarRepository _repository;
    private readonly IAccountService _accountService;

    public CarController(ICarRepository carRepo, IAccountService accountService)
    {
        _repository = carRepo;
        _accountService = accountService;
    }

    // GET: api/Car
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpGet("get")]
    public ActionResult<IEnumerable<CarDto>> GetCar()
    {
        var result = _repository.GetCar();
        if (result == null)
            return BadRequest();
        return Ok(result);
    }

    // GET: api/Car
    [HttpGet("get/{id}")]
    public ActionResult<CarDto> GetCar(Guid id)
    {
        var result = _repository.GetCar(id);
        if (result == null)
            return BadRequest();
        return Ok(result);
    }

    // PUT: api/Car/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
    [HttpPut("edit")]
    public ActionResult<CarDto> PutCar(Guid id, [FromBody]CarDto carDto)
    {
        var result = _repository.PutCar(id, carDto);
        if (result == null)
            return BadRequest($"Item could not be updated!");
        return Ok(result);
    }


    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
    [HttpPost("add")]
    public ActionResult<CarDto> AddCar([FromBody] CarDto carDto)
    {
        var result = _repository.SaveCar(carDto);
        if (result == null)
            return BadRequest();
        return Ok(result);
    }

    // DELETE: api/Car/5
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpDelete("delete")]
    public ActionResult<bool> DeleteCar(Guid id)
    {
        var result = _repository.DeleteCar(id);
        if (!result)
            return BadRequest($"Item could not be deleted, please try again");
        return Ok(result);
    }
}

