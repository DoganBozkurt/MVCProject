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
	public class UserManager : IUserService
	{
		IUserDal _userDal;

		public UserManager(IUserDal userDal)
		{
			_userDal = userDal;
		}

		public void TAdd(User t)
		{
			_userDal.Insert(t);
		}


		public User TGetById(int id)
		{
			return _userDal.GetById(id);
		}

		public void TRemove(User t)
		{
			throw new NotImplementedException();
		}

		public void TUpdate(User t)
		{
			throw new NotImplementedException();
		}

		public Task<User> TFindByEmailAsync(string email)
		{
			return _userDal.FindByEmailAsync(email);
		}
        public List<User> TGetAll()
        {
            return _userDal.GetAll();
        }

        public List<User> TUsersWithRoles()
        {
            return _userDal.GetUsersByRoles();
        }
    }
}
