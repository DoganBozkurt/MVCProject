using Microsoft.AspNetCore.Mvc;

namespace MVCProject.ViewComponents.Service
{
    public class Service:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
