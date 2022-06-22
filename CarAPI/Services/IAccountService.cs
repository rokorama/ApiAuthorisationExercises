using CarAPI.Models;

namespace CarAPI.Services;

public interface IAccountService
{
    public Account PostAccountToDb(string username, string password);
    public Account CreateAccount(string username, string password);
    public void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
    public bool Login(string username, string password, out string role);
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    public Account GetAccount(string username);
    public bool GrantAdminRights(string username);
    public bool RevokeAdminRights(string username);
}