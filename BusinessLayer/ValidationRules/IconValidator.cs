using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class IconValidator : AbstractValidator<Icon>
    {
        public IconValidator()
        {
            RuleFor(x => x.IIcon).NotEmpty().WithMessage("Lütfen bir ikon ekleyiniz");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Lütfen bir ikon başlığı seçiniz");
        }
    }
}
