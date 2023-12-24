using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        ContactManager _contactManager = new ContactManager(new EfContactDal());
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;

        public AdminController(UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult ListContact()
        {
            var values = _contactManager.TGetAll();
            return View(values);
        }
        public async Task<IActionResult> ListUserWithRoles()
        {
            var usersWithRoles = new List<UserWithRolesViewModel>();

            var roles = await _roleManager.Roles.ToListAsync();

            ViewBag.user = roles;
            var userList = await _userManager.Users.ToListAsync();

            foreach (var user in userList)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var roleIds = roles.Where(r => userRoles.Contains(r.Name)).Select(r => r.Id.ToString()).ToList();

                var userWithRoles = new UserWithRolesViewModel
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    UserName = user.UserName,
                    RoleIds = roleIds,
                    Roles = userRoles.ToList()
                };

                usersWithRoles.Add(userWithRoles);
            }

            return View(usersWithRoles);


        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(string roleName, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound($"Kullanıcı '{userName}' bulunamadı.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

            if (!string.IsNullOrEmpty(roleName))
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    return NotFound($"Rol '{roleName}' bulunamadı.");
                }

                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded)
                {
                    return BadRequest("Kullanıcı rolü güncellenemedi.");
                }
            }

            return Ok("Kullanıcı rolü başarıyla güncellendi.");
        }


    }
}
