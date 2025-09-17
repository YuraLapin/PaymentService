using Mediator;
using PaymentService.DataAccess.Postgres;
using PaymentService.DataAccess.Postgres.Models;
using PaymentService.WebApi.UseCases.Commands;

namespace OrderService.WebApi.UseCases.Handlers
{
    // <summary>
    // Обработчик для команды получения записи об оплате
    // </summary>
    public class GetPaymentHandler(DataBaseContext db) : IRequestHandler<GetPaymentCommand, Object>
    {
        // <summary>
        // Получает запись об оплате с заданным Id из БД
        // </summary>
        // <returns>
        // Полученный из БД Payment в виде Object при успехе
        // Строку сообщения об ошибке в виде Object при ошибке
        // </returns>
        // <param name="command">
        // Mediator команда с полем
        // PaymentId - Id требуемой записи об оплате
        // </param>
        // <param name="ct">
        // Токен отмены
        // </param>
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
