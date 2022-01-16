using Career.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Career.Identity.Data;

public class CareerIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public CareerIdentityDbContext(DbContextOptions<CareerIdentityDbContext> options)
        : base(options)
    {
    }
}