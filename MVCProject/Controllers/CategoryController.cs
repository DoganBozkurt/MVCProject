using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        CategoryManager _categoryManager = new CategoryManager(new EfCategoryDal());
        IconManager _iconManager = new IconManager(new EfIconDal());
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
			var values = _categoryManager.TGetCategoriesWithUserID(currentUser.Id);

            return View(values);
        }


        public IActionResult AddOrEdit(int id = 0)
        {
			ViewBag.IconData = _iconManager.TGetAll();
			if (id == 0)
                return View(new Category());
            else
            {
                var value = _categoryManager.TGetById(id);
                return View(value);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Category category)
        {
			ViewBag.IconData = _iconManager.TGetAll();
			var currentUser = await _userManager.GetUserAsync(User);
			category.UserID = currentUser.Id;
			CategoryValidator validations = new CategoryValidator(_categoryManager,currentUser.Id,category.Title, category.CategoryID);
            ValidationResult result =await validations.ValidateAsync(category);
            if (result.IsValid)
            {
				if (category.CategoryID == 0)
                {
					_categoryManager.TAdd(category);
                }
                else
                    _categoryManager.TUpdate(category);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            
            var category = _categoryManager.TGetById(id);
            if (category != null)
            {
                _categoryManager.TRemove(category);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}