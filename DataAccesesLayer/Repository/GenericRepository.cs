using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{

	public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Delete(T t)
        {
            using var c = new ContextDal();
            c.Remove(t);
            c.SaveChanges();
        }


        public void Insert(T t)
        {
            using var c = new ContextDal();
            c.Add(t);
            c.SaveChanges();
        }

        public void Update(T t)
        {
            using var c = new ContextDal();
            c.Update(t);
            c.SaveChanges();
        }
		public T GetById(int id)
		{
			using var c = new ContextDal();
			return c.Set<T>().Find(id);
		}
	}
}
