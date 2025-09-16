using Mediator;
using PaymentService.DataAccess.Postgres;
using PaymentService.DataAccess.Postgres.Models;
using PaymentService.WebApi.Services;
using PaymentService.WebApi.UseCases.Commands;

namespace OrderService.WebApi.UseCases.Handlers
{
    public class UpdatePaymentHandler(DataBaseContext db, ProducerService producer) : IRequestHandler<UpdatePaymentCommand, string?>
    {
        public async ValueTask<string?> Handle(UpdatePaymentCommand command, CancellationToken ct)
        {
            Payment? payment = await db.Payments.FindAsync(command.PaymentId, ct);
            if (payment == null)
            {
                return "Донных об оплате с заданным Id не существует";
            }

            if (command.Status)
            {
                producer.Produce("notification-topic", $"Оплата с Id {command.PaymentId} подтверждена");
            }

            payment.Status = command.Status;
            await db.SaveChangesAsync(ct);

            return null;
        }
    }
}
