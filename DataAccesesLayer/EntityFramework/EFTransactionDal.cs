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
        public List<Transaction> SelectedTransactions()
        {
            //son 7 gün
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;
            using (var C = new ContextDal())
            {

                return  C.Transactions.Include(x => x.Category)
                .Where(y => y.Date >= StartDate && y.Date >= EndDate)
                .ToList();
            }
        }
       public List<Transaction> TransactionsWithCategory()
        {
            using (var C = new ContextDal())
            {
                return C.Transactions.Include(x => x.Category)
                .ToList();
            }
        }

        //    public int TotalExpenseAsync(List<Transaction> selectedTransactions)
        //    {
        //        var result= selectedTransactions.Where(i => i.Category.Type == "Income")
        //            .Sum(j => j.Amount);
        //        return result;
        //    }

        //    public int TotalIncomeAsync(List<Transaction> selectedTransactions)
        //    {
        //        var result=selectedTransactions
        //            .Where(i => i.Category.Type == "Expense")
        //            .Sum(j => j.Amount);
        //        return result;
        //    }
    }
}
