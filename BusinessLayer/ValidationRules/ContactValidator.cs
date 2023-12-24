using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        private EmailValidator emailValidator = new EmailValidator();

        public ContactValidator()
        {
            RuleFor(x => x.Message).MaximumLength(100).WithMessage("Mesaj 100 karakterden fazla olamaz");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Lütfen mesajınızı giriniz");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Lütfen konu başlığı giriniz");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Lütfen Email giriniz")
                                  .MustAsync(async (email, cancellation) =>
                                  {
                                      if (!string.IsNullOrEmpty(email))
                                      {
                                          //return await  emailValidator.IsValidEmailAsync(email);
                                          return  emailValidator.IsValidEmailAsync(email);
                                      }
                                      return true; 
                                  }).WithMessage("Geçersiz e-mail adresi");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen isminizi giriniz");
        }

	}

}
