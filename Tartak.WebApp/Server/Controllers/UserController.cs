using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Tartak.WebApp.Server.Data;
using Tartak.WebApp.Shared.Models;

namespace Tartak.WebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public ApplicationUserModel GetCurrentUserInfo()
        {
            ApplicationUserModel applicationUserModel = new();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(x => x.Id == userId).Single();
            applicationUserModel.Id = user.Id;
            applicationUserModel.Name = user.UserName;
            applicationUserModel.Roles = _context.UserRoles.Where(x => x.UserId == user.Id).Join(_context.Roles, x => x.RoleId, y => y.Id, (x, y) => y.Name).ToList();
            return applicationUserModel;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> output = new List<ApplicationUserModel>();
            foreach (var user in _context.Users)
            {
                ApplicationUserModel applicationUserModel = new ApplicationUserModel();
                applicationUserModel.Id = user.Id;
                applicationUserModel.Name = user.UserName;

                applicationUserModel.Roles = _context.UserRoles.Where(x => x.UserId == user.Id).Join(_context.Roles, x => x.RoleId, y => y.Id, (x, y) => y.Name).ToList();

                output.Add(applicationUserModel);
            }
            return output;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllRoles")]
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
            var user = await _userManager.FindByIdAsync(data.UserId);
            await _userManager.AddToRoleAsync(user, data.RoleName);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/RemoveUserFromRole")]
        public async Task RemoveRole(UserAndRoleModel data)
        {
            string loggedInuserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(data.UserId);

            await _userManager.RemoveFromRoleAsync(user, data.RoleName);
        }
    }
}
