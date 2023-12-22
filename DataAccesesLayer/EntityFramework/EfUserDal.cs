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
	public class EfUserDal : GenericRepository<User>, IUserDal
	{
		public async Task<User> FindByEmailAsync(string email)
		{
			using (var c = new ContextDal())
			{
				return await c.Set<User>().FirstOrDefaultAsync(x => x.UserName == email);
			}
		}
	}
}
