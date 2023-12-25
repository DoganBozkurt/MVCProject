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
	public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
	{
		public List<Category> GetCategoriesWithUserID(int id)
		{
			using (var c = new ContextDal())
			{
				return c.Set<Category>().Include(y=> y.IconData).Where(x => x.UserID == id).ToList();
			}

		}


		//public Category GetById(int id, int userId)
		//{
		//	using var c = new ContextDal();
		//	return c.Set<Category>().Where(x => x.UserID == userId).
		//}
	}
}
