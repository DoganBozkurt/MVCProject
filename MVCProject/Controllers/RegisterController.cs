﻿using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
	[AllowAnonymous]
    public class RegisterController : Controller
    {
        readonly private UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        public RegisterController(UserManager<User> userManager, RoleManager<UserRole> roleManager)
		{
			_userManager = userManager;
            _roleManager = roleManager;
        }

		[HttpGet]
        public IActionResult Register()
        {
  
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Register(User p)
		{
			RegisterValidator validations = new RegisterValidator();
			ValidationResult results = await validations.ValidateAsync(p);

            if (results.IsValid)
            {
				var resource=Directory.GetCurrentDirectory();
				var extension=Path.GetExtension(p.Picture.FileName);
				var imageName = Guid.NewGuid() + extension;
				var saveLocation = resource + "/wwwroot/userImage/" + imageName;
				var stream = new FileStream(saveLocation, FileMode.Create);
				await p.Picture.CopyToAsync(stream);
				User user = new User()
				{
					UserName = p.Mail,
					Name = p.Name,
					Surname = p.Surname,
					ImageUrl = imageName,
					Password = p.Password,
				};
				var result = await _userManager.CreateAsync(user, p.Password);

                if (result.Succeeded)
                {
                    TempData["SuccessRegister"] = "Kayıt Başarılı";

                    // Rol tablosunu kontrol edip rol atama işlemini gerçekleştirme
                    if (!_roleManager.Roles.Any())
                    {
                        // Rol tablosu boş ise, kullanıcıya "Admin" rolünü ata
                        var adminRole = new UserRole { Name = "Admin" };
                        await _roleManager.CreateAsync(adminRole);
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        // Rol tablosu boş değilse, kullanıcıya "User" rolünü ata
                        var userRole = new UserRole { Name = "User" };
                        await _roleManager.CreateAsync(userRole);
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                }
                else
                {
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Code);
						if (item.Code == "DuplicateUserName")
						{
							TempData["fieldRegister"] = "Bu e-posta adresi zaten kayıtlı. Lütfen farklı bir e-posta adresi kullanın.";

						}
						else
						{
							TempData["fieldRegister"] = "Bir sorun oluştu geliştirici ile iletişime geçiniz.";
						}
					}

				}

            }
			else
			{
				var errorMessage = "";
				foreach (var item in results.Errors)
				{
					errorMessage = item.ErrorMessage;
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
				TempData["fieldRegister"] = errorMessage;
			}
			return View();
		}

	}
}
