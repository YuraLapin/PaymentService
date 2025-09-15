using Mediator;
using PaymentService.DataAccess.Postgres;
using PaymentService.DataAccess.Postgres.Models;
using PaymentService.WebApi.UseCases.Commands;

namespace OrderService.WebApi.UseCases.Handlers
{
    public class UpdatePaymentHandler(DataBaseContext db) : IRequestHandler<UpdatePaymentCommand, string?>
    {
        public async ValueTask<string?> Handle(UpdatePaymentCommand command, CancellationToken ct)
        {
            //if (command.PaymentId < 0) return "";

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
