using CarAPI.Models;

namespace CarAPI.Services;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AccountRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Account AddNewAccount(Account account)
    {
        _dbContext.Accounts.Add(account);
        if (_dbContext.SaveChanges() > 0)
            return account;
        else
            return null;
    }

    public Account GetAccount(string username)
    {
        return _dbContext.Accounts.FirstOrDefault(x => x.Username == username);
    }

    public bool GrantAdminRights(string username)
    {
        var dbEntry = _dbContext.Accounts.SingleOrDefault(a => a.Username == username);
        if (dbEntry.Role == "Admin")
            return true;
        dbEntry.Role = "Admin";
        _dbContext.Entry(dbEntry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        try
        {
            _dbContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool RevokeAdminRights(string username)
    {
        var dbEntry = _dbContext.Accounts.SingleOrDefault(a => a.Username == username);
        if (dbEntry.Role == "User")
            return true;
        dbEntry.Role = "User";
        _dbContext.Entry(dbEntry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        try
        {
            _dbContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}