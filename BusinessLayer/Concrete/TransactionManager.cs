using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TransactionManager : ITransactionService
    {
        ITransactionDal _transactionDal;

        public TransactionManager(ITransactionDal transactionDal)
        {
            _transactionDal = transactionDal;
        }

        public void TAdd(Transaction t)
        {
            _transactionDal.Insert(t);
        }

        public List<Transaction> TGetAll()
        {
            return _transactionDal.GetList();
        }

        public Transaction TGetById(int id)
        {
            return _transactionDal.GetById(id);
        }

        public void TRemove(Transaction t)
        {
            _transactionDal.Delete(t);
        }

        public void TUpdate(Transaction t)
        {
            _transactionDal.Update(t);
        }

        public List<Transaction> TSelectedTransactions()
        {
            return _transactionDal.SelectedTransactions();
        }

        public List<Transaction> TTransactionsWithCategory()
        {
            return _transactionDal.TransactionsWithCategory();
        }
    }
}
