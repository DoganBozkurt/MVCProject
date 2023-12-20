using Microsoft.AspNetCore.Mvc;

namespace MVCProject.ViewComponents.Team
{
    public class Team : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
