using Microsoft.AspNetCore.Mvc;
using CarAPI.Models;
using CarAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CarAPI.Authorization;

namespace AccountAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IJwtService _jwtService;

    public AccountController(IAccountService accountService, IJwtService jwtService)
    {
        _accountService = accountService;
        _jwtService = jwtService;
    }

    // PUT: api/Account/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [AllowAnonymous]
    [HttpPost("signup")]
    public ActionResult<Account> PutAccount([FromQuery]string username, [FromQuery]string password)
    {
        var result = _accountService.PostAccountToDb(username, password);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<string> LoginToAccount(AccountDto request)
    {
        if (!_accountService.Login(request.Username, request.Password, out string role))
            return BadRequest($"Incorrect credentials");
        string token = _jwtService.GetJwtToken(request.Username, role);
        return Ok(token);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPut("grantAdmin")]
    public ActionResult<bool> GrantAdminRights(string username)
    {
        if (!_accountService.GrantAdminRights(username))
            return false;
        return true;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPut("revokeAdmin")]
    public ActionResult<bool> RevokeAdminRights(string username)
    {
        if (!_accountService.RevokeAdminRights(username))
            return false;
        return true;
    }
    
    [ApiKeyAuth]
    [HttpGet("externalCall")]
    public string ApiKeyGet()
    {
        return "Hello from port 7290!";
    }
}

