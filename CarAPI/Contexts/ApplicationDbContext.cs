using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarAPI.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CarAPI.Models.Car> Cars { get; set; }
    public DbSet<CarAPI.Models.Account> Accounts { get; set; }
}
