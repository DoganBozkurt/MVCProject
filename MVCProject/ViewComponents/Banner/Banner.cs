using Microsoft.AspNetCore.Mvc;

namespace MVCProject.ViewComponents.Banner
{
    public class Banner: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
