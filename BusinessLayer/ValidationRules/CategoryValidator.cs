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

        public CategoryValidator(CategoryManager categoryManager,string title)
        {


            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Lütfen kategori başlığı giriniz")
                .Must((catgory) => !CategoryWithTitleExists(categoryManager,title))
                .WithMessage("Bu başlıkta bir kategori zaten mevcut");

            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("Lütfen bir icon seçiniz");
        }

        private bool CategoryWithTitleExists(CategoryManager categoryManager,string title)
        {
            return categoryManager.TGetAll().Any(x => x.Title == title);
        }
    }
}
