using AspWebProject.Models;
using FluentValidation;

namespace AspWebProject.FluentValidators
{
    public class CustomerValidator:AbstractValidator<Customer>
    {

        public CustomerValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name alanı boş bırakılamaz");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş bırakılamaz").EmailAddress().
                WithMessage("Email alanı doğru formatta olmalıdır");

            RuleFor(x => x.Age).NotEmpty().WithMessage("Age alanı boş bırakılamaz").InclusiveBetween(18, 60).
                WithMessage("18-60 arasında olmalıdır.");
        }
    }
}
