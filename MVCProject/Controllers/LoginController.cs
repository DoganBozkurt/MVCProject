using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		readonly private SignInManager<User> _signInManager;

		UserManager userManager = new UserManager(new EfUserDal());

		public LoginController(SignInManager<User> signInManager)
		{
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Login()
		{
            return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(User p)
		{

            var errorMessage = "";
			if (ModelState.IsValid) // Kullanıcı modeli geçerli mi?
			{
				var result = await _signInManager.PasswordSignInAsync(p.Mail, p.Password, true, true);

				if (result.Succeeded)
				{
					var user = await userManager.TFindByEmailAsync(p.Mail); // Kullanıcıyı e-posta ile bul
					if (user != null)
					{
						// Profil resmini görüntüleme
                        // Kullanıcı girişi başarılıysa, kullanıcı bilgilerini session'a ata
                        HttpContext.Session.SetString("UserName", user.Name);
                        HttpContext.Session.SetString("UserProfileImage", user.ImageUrl);
                    }
					return RedirectToAction("Index", "Dashboard");
				}
				else if (result.IsLockedOut) // Hesap kilitlenmiş mi?
				{
					errorMessage = "Çok fazla başarısız giriş deneyimi, hesabınız kısa süreliğine kitlenmiştir.";
				}
				else
				{
					errorMessage = "Yanlış e-mail veya şifre lütfen bilgilerinizi kontrol ediniz";
				}
			}
            else
            {
				errorMessage = "Geçersiz giriş denemesi";
			}
			TempData["fieldLogin"] = errorMessage;
			return View();
		}
		
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}
    }
}
