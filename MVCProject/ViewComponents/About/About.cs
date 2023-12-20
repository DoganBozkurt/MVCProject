using Microsoft.AspNetCore.Mvc;

namespace MVCProject.ViewComponents.About
{
    public class About : ViewComponent
    {
        public IViewComponentResult Invoke() 
        {
            return View();
        }
    }
}
