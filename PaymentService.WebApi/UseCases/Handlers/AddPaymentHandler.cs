using Mediator;
using PaymentService.DataAccess.Postgres;
using PaymentService.WebApi.UseCases.Commands;
using PaymentService.WebApi.Utility;

namespace OrderService.WebApi.UseCases.Handlers
{
    public class AddPaymentHandler(DataBaseContext db, InputChecker inputChecker) : IRequestHandler<AddPaymentCommand, Object>
    {
        public async ValueTask<Object> Handle(AddPaymentCommand command, CancellationToken ct)
        {
            string? errorMessage = inputChecker.CheckPayment(command.Payment);
            if (errorMessage != null) return errorMessage;

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
