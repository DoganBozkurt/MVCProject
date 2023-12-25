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
    public class IconManager : IIconService
    {
        IIconDal _iconDal;

        public IconManager(IIconDal iconDal)
        {
            _iconDal = iconDal;
        }

        public void TAdd(Icon t)
        {
            _iconDal.Insert(t);
        }

        public List<Icon> TGetAll()
        {
            return _iconDal.GetAll();
        }

        public Icon TGetById(int id)
        {
            return _iconDal.GetById(id);
        }

        public void TRemove(Icon t)
        {
            _iconDal.Delete(t);
        }

        public void TUpdate(Icon t)
        {
            _iconDal.Update(t);
        }
    }
}
