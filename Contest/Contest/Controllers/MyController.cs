using Contest.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Contest.Controllers
{
    [ApiController]
    public class MyController : ControllerBase
    {
        protected Guid? GetUserId()
        {
            if (User.Identity!.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null)
                {
                    return new Guid(userId);
                }
            };

            return null;
        }

        protected string? GetAccountType(ApplicationDbContext context)
        {
            if (User.Identity!.IsAuthenticated)
            {
                var user = context.Users.First(u => u.UserName == User.Identity.Name);
                return user.AccountType;
            }

            return null;
        }
    }
}
