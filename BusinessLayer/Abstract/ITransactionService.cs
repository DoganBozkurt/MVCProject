using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITransactionService:IGenericService<Transaction>
    {
        List<Transaction> TSelectedTransactions(int userId);
        List<Transaction> TTransactionsWithCategory(int id);
		List<Transaction> TGetTransactionsWithUserID(int userId);
	}
}
