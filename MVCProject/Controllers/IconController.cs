using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace MVCProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class IconController : Controller
    {
        IconManager _iconManager = new IconManager(new EfIconDal());
        public IActionResult Index()
        {
           var values= _iconManager.TGetAll();
            return View(values);
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Icon());
            else
            {
                var value = _iconManager.TGetById(id);
                if (value == null)
                {
                    return View(new Icon());
                }
                return View(value);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Icon icon)
        {
            IconValidator validations = new IconValidator();
            ValidationResult result = await validations.ValidateAsync(icon);
            if (result.IsValid)
            {

                if (icon.IconID == 0)
                {
                    _iconManager.TAdd(icon);
                }
                else
                {
                    _iconManager.TUpdate(icon);
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(icon);

            }

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var icon = _iconManager.TGetById(id);
            if (icon != null)
            {
                _iconManager.TRemove(icon);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
