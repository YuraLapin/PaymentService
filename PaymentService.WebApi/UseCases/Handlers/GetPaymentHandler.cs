using Mediator;
using PaymentService.DataAccess.Postgres;
using PaymentService.DataAccess.Postgres.Models;
using PaymentService.WebApi.UseCases.Commands;

namespace OrderService.WebApi.UseCases.Handlers
{
    public class GetPaymentHandler(DataBaseContext db) : IRequestHandler<GetPaymentCommand, Object>
    {
        public async ValueTask<Object> Handle(GetPaymentCommand command, CancellationToken ct)
        {
            Payment? res = await db.Payments.FindAsync(command.PaymentId, ct);
            if (res == null)
            {
                return "Донных об оплате с заданным Id не существует";
            }
            return res;
        }
    }
}
