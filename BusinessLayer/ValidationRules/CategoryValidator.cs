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

        public CategoryValidator(CategoryManager categoryManager,string title, int id)
        {


            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Lütfen kategori başlığı giriniz")
                .Must((catgory) => !CategoryWithTitleExists(categoryManager,title,id))
                .WithMessage("Bu başlıkta bir kategori zaten mevcut");

            RuleFor(x => x.IconID)
                .NotEmpty().WithMessage("Lütfen bir icon seçiniz");
        }

        private bool CategoryWithTitleExists(CategoryManager categoryManager,string title, int id)
        {
            if (id==0)
            {
				return categoryManager.TGetAll().Any(x => x.Title == title);
			}
            return false;
        }
    }
}
