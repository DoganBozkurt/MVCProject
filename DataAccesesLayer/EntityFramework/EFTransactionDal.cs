using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
	public class EFTransactionDal : GenericRepository<Transaction>, ITransactionDal
	{
		public List<Transaction> SelectedTransactions(int userId)
		{
			//son 7 gün
			DateTime StartDate = DateTime.Today.AddDays(-6);
			DateTime EndDate = DateTime.Today;
			using (var C = new ContextDal())
			{

				return C.Transactions.Include(x => x.Category)
				.Where(y => y.UserID==userId && y.Date >= StartDate && y.Date <= EndDate)
				.ToList();
			}
		}
		public List<Transaction> TransactionsWithCategory(int id)
		{
			using (var C = new ContextDal())
			{
				return C.Transactions.Include(x => x.Category).Where(x => x.UserID == id)
				.ToList();
			}
		}

		public List<Transaction> GetTransactionsWithUserID(int userId)
		{
			using (var c = new ContextDal())
			{
				return c.Set<Transaction>().Where(x => x.UserID == userId).ToList();
			}
		}

	}
}
