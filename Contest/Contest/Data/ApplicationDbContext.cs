using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Contest.Shared;

namespace Contest.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Contest.Shared.AccountType> AccountType { get; set; } = default!;
        public DbSet<Contest.Shared.SecretQuestion> SecretQuestion { get; set; } = default!;
        public DbSet<Contest.Shared.Tutorial> Tutorial { get; set; } = default!;
        public DbSet<Contest.Shared.Class> Class { get; set; } = default!;
        public DbSet<Contest.Shared.ClassUser> ClassUser { get; set; } = default!;
        public DbSet<Contest.Shared.Lesson> Lesson { get; set; } = default!;
        public DbSet<Contest.Shared.Progress> Progress { get; set; } = default!;
        public DbSet<Contest.Shared.Quiz> Quiz { get; set; } = default!;
    }
}
