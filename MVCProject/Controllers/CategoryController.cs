using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    public class CategoryController : Controller
    {
        Category2Manager category2 = new Category2Manager(new EfCategory2Dal());

        public IActionResult Index()
        {
            var values = category2.TGetAll();

            return View(values);
        }


        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Category2());
            else
            {
                var value = category2.TGetById(id);
                return View(value);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("CategoryID,Title,Icon,Type")] Category2 category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryID == 0)
                    category2.TAdd(category);
                else
                    category2.TUpdate(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            
            var category = category2.TGetById(id);
            if (category != null)
            {
                category2.TRemove(category);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}