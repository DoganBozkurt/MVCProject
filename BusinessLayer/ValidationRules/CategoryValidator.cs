using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Linq;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {

        public CategoryValidator(CategoryManager categoryManager,int userID,string title, int id)
        {


            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Lütfen kategori başlığı giriniz")
                .Must((catgory) => !CategoryWithTitleExists(categoryManager,userID,title,id))
                .WithMessage("Bu başlıkta bir kategorin zaten mevcut");

            RuleFor(x => x.IconID)
                .NotEmpty().WithMessage("Lütfen bir icon seçiniz");
        }

        private bool CategoryWithTitleExists(CategoryManager categoryManager, int userID,string title, int id)
        {
            if (id==0)
            {
				return categoryManager.TGetCategoriesWithUserID(userID).Any(x => x.Title.ToLower() == title.ToLower());
			}
            return false;
        }
    }
}
