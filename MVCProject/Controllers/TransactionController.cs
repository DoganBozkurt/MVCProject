
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.EntityFrameworkCore;
using Transaction = EntityLayer.Concrete.Transaction;
using TransactionManager = BusinessLayer.Concrete.TransactionManager;
using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.EntityFramework;

namespace MVCProject.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        TransactionManager transactionManager = new TransactionManager(new EFTransactionDal());
		CategoryManager category2 = new CategoryManager(new EfCategoryDal());
        readonly private UserManager<User> _userManager;

		public TransactionController(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
        {
			var currentUser = await _userManager.GetUserAsync(User);
			var values = transactionManager.TTransactionsWithCategory(currentUser.Id);
            return View(values);
        }

        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
           await PopulateCategories();
            if (id == 0)
                return View(new Transaction());
            else
            {
                var value = transactionManager.TGetById(id); 
                if (value == null)//This condition to catch the exception in the view ;)
                {
                    return View(new Transaction());
                }
                return View(value);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryID,Amount,Note,Date")] Transaction transaction)
        {
			if (ModelState.IsValid)
			{

				if (transaction.TransactionId == 0)
				{
					var currentUser = await _userManager.GetUserAsync(User);
					transaction.UserID = currentUser.Id;
					transactionManager.TAdd(transaction);
				}
				else
				{
					var currentUser = await _userManager.GetUserAsync(User);
					transaction.UserID = currentUser.Id;
					transactionManager.TUpdate(transaction);
				}

				return RedirectToAction(nameof(Index));
			}
			await PopulateCategories();
			return View(transaction);

		}

		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var transaction = transactionManager.TGetById(id);
            if (transaction != null)
            {
                transactionManager.TRemove(transaction);
            }

            return RedirectToAction(nameof(Index));
        }


        [NonAction]
        public async Task PopulateCategories()
        {
			var currentUser = await _userManager.GetUserAsync(User);
			var kategoriSec = category2.TGetCategoriesWithUserID(currentUser.Id);
            Category DefaultCategory = new Category() { CategoryID = 0, Title = "Choose a Category" };
            ViewBag.Categories = kategoriSec;
        }
    }

}



