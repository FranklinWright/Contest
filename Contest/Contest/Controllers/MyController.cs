using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Contest.Controllers
{
    [ApiController]
    [Authorize]
    public class MyController : ControllerBase
    {
        protected string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
