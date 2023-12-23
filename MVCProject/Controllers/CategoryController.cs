using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        CategoryManager category2 = new CategoryManager(new EfCategoryDal());
		readonly private UserManager<User> _userManager;

		public CategoryController(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
        {
            //var b = 0;
            //var a = 25 / b;
            var currentUser = await _userManager.GetUserAsync(User);
			var values = category2.TGetCategoriesWithUserID(currentUser.Id);

            return View(values);
        }


        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Category());
            else
            {
                var value = category2.TGetById(id);
                return View(value);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryID,Title,Icon,Type")] Category category)
        {
            if (ModelState.IsValid)
            {
				var currentUser = await _userManager.GetUserAsync(User);
				category.UserID = currentUser.Id;
				if (category.CategoryID == 0)
                {
					category2.TAdd(category);
                }
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