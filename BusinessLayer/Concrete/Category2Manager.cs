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
    public class Category2Manager : ICategory2Service
    {
        ICategory2Dal category2dal;

        public Category2Manager(ICategory2Dal category2dal)
        {
            this.category2dal = category2dal;
        }

        public void TAdd(Category2 t)
        {
            category2dal.Insert(t);
        }

        public List<Category2> TGetAll()
        {
           return category2dal.GetList();
        }

        public Category2 TGetById(int id)
        {
            return category2dal.GetById(id);
        }

        public void TRemove(Category2 t)
        {
            category2dal.Delete(t);
        }

        public void TUpdate(Category2 t)
        {
            category2dal.Update(t);
        }
    }
}
