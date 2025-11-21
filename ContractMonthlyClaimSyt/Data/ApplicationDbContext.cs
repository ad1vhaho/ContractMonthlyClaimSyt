// Data/ApplicationDbContext.cs
using ContractMonthlyClaimSyt.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractMonthlyClaimSyt.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims => Set<Claim>();
    }
}
