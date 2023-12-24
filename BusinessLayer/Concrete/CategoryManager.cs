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
    public class CategoryManager : ICategoryService
    {
        ICategoryDal category2dal;

        public CategoryManager(ICategoryDal category2dal)
        {
            this.category2dal = category2dal;
        }

        public void TAdd(Category t)
        {
            category2dal.Insert(t);
        }


        public void TRemove(Category t)
        {
            category2dal.Delete(t);
        }

        public void TUpdate(Category t)
        {
            category2dal.Update(t);
        }

        public List<Category> TGetCategoriesWithUserID(int id)
        {
            return category2dal.GetCategoriesWithUserID(id);
        }

        public Category TGetById(int id)
        {
            return category2dal.GetById(id);
        }

        public List<Category> TGetAll()
        {
            return category2dal.GetAll();
        }

    }
}
