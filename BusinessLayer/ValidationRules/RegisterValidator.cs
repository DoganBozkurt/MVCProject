using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
	public class RegisterValidator : AbstractValidator<User>
	{
		private EmailValidator emailValidator = new EmailValidator();

		public RegisterValidator()
		{
			RuleFor(x => x.Mail).NotEmpty().WithMessage("Lütfen Email giriniz")
					  .MustAsync(async (email, cancellation) =>
					  {
						  if (!string.IsNullOrEmpty(email))
						  {
							  //return await  emailValidator.IsValidEmailAsync(email);
							  return emailValidator.IsValidEmailAsync(email);
						  }
						  return true;
					  }).WithMessage("Geçersiz e-mail adresi");
			RuleFor(x=> x.Name).NotEmpty().WithMessage("Lütfen adınızı giriniz");
			RuleFor(x=> x.Surname).NotEmpty().WithMessage("Lütfen soyadınız giriniz");
			RuleFor(x=> x.Picture).NotEmpty().WithMessage("Lütfen profil resmi seçiniz");
			RuleFor(x=> x.Password).NotEmpty().WithMessage("Lütfen şifenizi giriniz");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Lütfen şifenizi giriniz").MinimumLength(6).WithMessage("Şifre en az 6 karakter uzunluğunda olmalıdır");
		}
	}
}
