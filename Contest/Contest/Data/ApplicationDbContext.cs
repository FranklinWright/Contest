using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Contest.Shared;

namespace Contest.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Contest.Shared.AccountType> AccountType { get; set; } = default!;
        public DbSet<Contest.Shared.SecretQuestion> SecretQuestion { get; set; } = default!;
    }
}
