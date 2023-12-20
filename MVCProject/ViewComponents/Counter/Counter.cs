using Microsoft.AspNetCore.Mvc;

namespace MVCProject.ViewComponents.Counter
{
    public class Counter:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
