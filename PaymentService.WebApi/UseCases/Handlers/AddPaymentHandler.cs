using FluentValidation;
using Mediator;
using PaymentService.DataAccess.Postgres;
using PaymentService.WebApi.Models;
using PaymentService.WebApi.UseCases.Commands;

namespace OrderService.WebApi.UseCases.Handlers
{
    public class AddPaymentHandler(DataBaseContext db, IValidator<Payment> validator) : IRequestHandler<AddPaymentCommand, Object>
    {
        public async ValueTask<Object> Handle(AddPaymentCommand command, CancellationToken ct)
        {
            var validationResult = await validator.ValidateAsync(command.Payment, ct);
            if (!validationResult.IsValid) return validationResult.ToString();

            var newPayment = new PaymentService.DataAccess.Postgres.Models.Payment()
            {
                OrderId = command.Payment.OrderId,
                Price = command.Payment.Price,
                Status = false,
                DateCreated = DateTime.UtcNow,
            };
            db.Payments.Add(newPayment);
            await db.SaveChangesAsync(ct);

            return newPayment.Id;
        }
    }
}
