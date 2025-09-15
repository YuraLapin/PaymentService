using FluentValidation;
using PaymentService.WebApi.Models;

namespace PaymentService.WebApi.Validators
{
    public class PaymentValidator: AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.OrderId).NotEmpty().WithMessage("Поле OrderId должно быть заполнено");
            RuleFor(p => p.OrderId).GreaterThan(-1).WithMessage("В поле OrderId должно стоять не отрицательное значение");

            RuleFor(p => p.Price).NotEmpty().WithMessage("Поле Price должно быть заполнено");
            RuleFor(p => p.Price).GreaterThan(-1).WithMessage("В поле Price должно стоять не отрицательное значение");
        }
    }
}
