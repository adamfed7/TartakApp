using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Tartak.WebApp.Library.DataAccess;
using Tartak.WebApp.Server.Data;
using Tartak.WebApp.Server.Models;
using TRMDataManager.Library.Models;

namespace Tartak.WebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserData _userData;
        private readonly ILogger<UserController> _logger;

        public UserController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IUserData userData,
            ILogger<UserController> logger)
        {
            _context = context;
            _userManager = userManager;
            _userData = userData;
            _logger = logger;
        }
        [HttpGet]
        public UserModel GetCurrentUserInfo()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userData.GetUserByID(userId).Single();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllUsers")]
        public List<ApplicationUser> GetAllUsers()
        {
            List<ApplicationUser> output = new List<ApplicationUser>();

            foreach (var user in _context.Users)
            {
                ApplicationUser applicationUserModel = new ApplicationUser();
                applicationUserModel.Id = user.Id;
                applicationUserModel.Name = user.UserName;

                applicationUserModel.Roles = _context.UserRoles.Where(x => x.UserId == user.Id).Join(_context.Roles, x => x.RoleId, y => y.Id, (x, y) => y.Name).ToList();

                output.Add(applicationUserModel);
            }
            return output;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            var output = _context.Roles.ToDictionary(x => x.Id, x => x.Name);
            return output;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/AdduserToRole")]
        public async Task AddRole(UserAndRoleModel data)
        {
            string loggedInuserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedUser = _userData.GetUserByID(loggedInuserId).Single();
            var user = await _userManager.FindByIdAsync(data.UserId);

            _logger.LogInformation("Admin {Admin} added user {User} to role {Role}",
                loggedUser.Id, user.Id, data.RoleName);
            await _userManager.AddToRoleAsync(user, data.RoleName);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/RemoveUserFromRole")]
        public async Task RemoveRole(UserAndRoleModel data)
        {
            string loggedInuserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedUser = _userData.GetUserByID(loggedInuserId).Single();

            var user = await _userManager.FindByIdAsync(data.UserId);

            _logger.LogInformation("Admin {Admin} removed user {User} from role {Role}",
                loggedUser.Id, user.Id, data.RoleName);
            await _userManager.RemoveFromRoleAsync(user, data.RoleName);
        }
    }
}
