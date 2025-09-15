using Mediator;
using PaymentService.DataAccess.Postgres;
using PaymentService.DataAccess.Postgres.Models;
using PaymentService.WebApi.UseCases.Commands;
using PaymentService.WebApi.Utility;

namespace OrderService.WebApi.UseCases.Handlers
{
    public class UpdatePaymentHandler(DataBaseContext db, InputChecker inputChecker) : IRequestHandler<UpdatePaymentCommand, string?>
    {
        public async ValueTask<string?> Handle(UpdatePaymentCommand command, CancellationToken ct)
        {
            string? errorMessage = inputChecker.CheckId(command.PaymentId);
            if (errorMessage != null) return errorMessage;

            Payment? payment = await db.Payments.FindAsync(command.PaymentId, ct);
            if (payment == null)
            {
                return "Донных об оплате с заданным Id не существует";
            }

            payment.Status = command.Status;
            await db.SaveChangesAsync(ct);

            return null;
        }
    }
}
