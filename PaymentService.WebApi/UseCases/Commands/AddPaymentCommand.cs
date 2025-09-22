using Mediator;
using PaymentService.WebApi.Models;

namespace PaymentService.WebApi.UseCases.Commands
{
    /// <summary>
    /// Mediator команда для записи данных об оплате в БД
    /// </summary>
    /// <returns>
    /// Id добавленного записи в виде Object при успехе
    /// Сообщение об ошибке в виде Object при ошибке
    /// </returns>
    /// <param name="Payment">
    /// Объект добавляемой записи об оплате
    /// </param>
    public sealed record class AddPaymentCommand(Payment Payment) : IRequest<Object>;
}
