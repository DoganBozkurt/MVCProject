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
       public List<Transaction> SelectedTransactions();
        public List<Transaction> TransactionsWithCategory();
        //int TotalIncomeAsync(List<Transaction> selectedTransactions);
        //int TotalExpenseAsync(List<Transaction> selectedTransactions);
    }
}
