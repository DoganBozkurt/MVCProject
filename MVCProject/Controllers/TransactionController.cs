using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transaction = EntityLayer.Concrete.Transaction;
using TransactionManager = BusinessLayer.Concrete.TransactionManager;

namespace MVCProject.Controllers
{
    public class TransactionController : Controller
    {
        TransactionManager transactionManager = new TransactionManager(new EFTransactionDal());
		Category2Manager category2 = new Category2Manager(new EfCategory2Dal());
		public  IActionResult Index()
        {
            var values = transactionManager.TTransactionsWithCategory();
            return View(values);
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            PopulateCategories();
            if (id == 0)
                return View(new Transaction());
            else
            {
                var value = transactionManager.TGetById(id);
                return View(value);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("TransactionId,CategoryID,Amount,Note,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if (transaction.TransactionId == 0)
                    transactionManager.TAdd(transaction);
                else
                { transactionManager.TUpdate(transaction); }

                return RedirectToAction(nameof(Index));
            }
            PopulateCategories();
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
        public void PopulateCategories()
        {
			var kategoriSec = category2.TGetAll();
            Category2 DefaultCategory = new Category2() { CategoryID = 0, Title = "Choose a Category" };
            ViewBag.Categories = kategoriSec;
        }
    }

}



