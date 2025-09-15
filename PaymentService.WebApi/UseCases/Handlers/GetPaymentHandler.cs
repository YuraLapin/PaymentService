using Mediator;
using PaymentService.DataAccess.Postgres;
using PaymentService.DataAccess.Postgres.Models;
using PaymentService.WebApi.UseCases.Commands;
using PaymentService.WebApi.Utility;

namespace OrderService.WebApi.UseCases.Handlers
{
    public class GetPaymentHandler(DataBaseContext db, InputChecker inputChecker) : IRequestHandler<GetPaymentCommand, Object>
    {
        public async ValueTask<Object> Handle(GetPaymentCommand command, CancellationToken ct)
        {
            string? errorMessage = inputChecker.CheckId(command.PaymentId);
            if (errorMessage != null) return errorMessage;

            Payment? res = await db.Payments.FindAsync(command.PaymentId, ct);
            if (res == null)
            {
                return "Донных об оплате с заданным Id не существует";
            }
            return res;
        }
    }
}
