using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ITransactionDal : IGenericDal<Transaction>
    {
       public List<Transaction> SelectedTransactions(int userId);
        public List<Transaction> TransactionsWithCategory(int id);
		List<Transaction> GetTransactionsWithUserID(int userId);
		//int TotalIncomeAsync(List<Transaction> selectedTransactions);
		//int TotalExpenseAsync(List<Transaction> selectedTransactions);
	}
}
