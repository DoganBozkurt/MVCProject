using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        ContactManager contactManager = new ContactManager(new EfContactDal());
        public IActionResult Index()
        {
            //var b = 0;
            //var a = 25 / b;
            return View();
        }
        public IActionResult Head()
        {
            return View();
        }
        public IActionResult Navbar()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SendMessageContact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessageContact(Contact contact)
        {
            ContactValidator validations = new ContactValidator();
            ValidationResult results = await validations.ValidateAsync(contact);

            if (results.IsValid)
            {
                contactManager.TAdd(contact);
                TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi!";
                return Redirect("/Default/Index#contact");
            }
            else
            {
                var errorMessage = "";

                foreach (var item in results.Errors)
                {
                    errorMessage = item.ErrorMessage;
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                TempData["field"] = errorMessage;
                TempData["ErrorCode"] = "Error1";
                return Redirect("/Default/Index#contact");
            }
        }


        public IActionResult Footer()
        {
            return View();
        }
    }
}
