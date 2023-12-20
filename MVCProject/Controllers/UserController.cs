using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    public class UserController : Controller
    {
        UserManager userManager = new UserManager(new EfUserDal());
        public IActionResult Index()
        {
            var values = userManager.TGetAll();
            return View(values);
        }
        public IActionResult Head()
        {
            return View();
        }
        public IActionResult Navbar()
        {
            return View();
        }
        public IActionResult Footer()
        {
            return View();
        }
    }
}
