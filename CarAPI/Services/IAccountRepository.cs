using CarAPI.Models;

namespace CarAPI.Services;

public interface IAccountRepository
{
    public Account AddNewAccount(Account account);
    public Account GetAccount(string username);
    public bool GrantAdminRights(string username);
    public bool RevokeAdminRights(string username);
}