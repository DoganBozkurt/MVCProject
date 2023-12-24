using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class TransacionValidator : AbstractValidator<Transaction>
    {
        public TransacionValidator()
        {
            RuleFor(x => x.Date).NotEmpty().WithMessage("Lütfen bir tarih seçiniz");
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage("Lütfen bir kategoriyi seçiniz");
			RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Tutar 0'dan büyük olmalıdır");
			RuleFor(x => x.Amount).NotEmpty().WithMessage("Lütfen bir tutar giriniz");
            RuleFor(x => x.Note).NotEmpty().WithMessage("Lütfen bir not giriniz");

        }

    }
}
