using System.Security.Cryptography;
using CarAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarAPI.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepo;
    private readonly IJwtService _jwtService;
    public AccountService(IAccountRepository accountRepo, IJwtService jwtService)
    {
        _accountRepo = accountRepo;
        _jwtService = jwtService;
    }

    public Account GetAccount(string username)
    {
        return _accountRepo.GetAccount(username);
    }

    public Account PostAccountToDb(string username, string password)
    {
        var account = CreateAccount(username, password);
        _accountRepo.AddNewAccount(account);
        return account;
    }

    public Account CreateAccount(string username, string password)
    {
        CreatePassword(password, out byte[] passwordHash, out byte[] passwordSalt);
        var account = new Account()
        {
            Username = username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = "User"
        };
        return account;
    }

    public void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public bool Login(string username, string password, out string role)
    {
        var account = GetAccount(username);
        if (account != null)
        {
            role = account.Role;
            return true;
        }
        role = null;
        return false;

    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        return true;
    }

    public bool GrantAdminRights(string username)
    {
        return _accountRepo.GrantAdminRights(username);
    }

    public bool RevokeAdminRights(string username)
    {
        return _accountRepo.RevokeAdminRights(username);
    }
}