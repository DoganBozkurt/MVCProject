using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error404()
        {
            return View();
        }
    }
}
