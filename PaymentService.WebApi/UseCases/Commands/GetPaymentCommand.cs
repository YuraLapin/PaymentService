using Mediator;

namespace PaymentService.WebApi.UseCases.Commands
{
    // <summary>
    // Mediator команда для получения записи об оплате из БД
    // </summary>
    // <returns>
    // Полученная из БД Payment в виде Object при успехе
    // Строку сообщения об ошибке в виде Object при ошибке
    // </returns>
    // <param name="PaymentId">
    // Id требуемой записи
    // </param>
    public sealed record class GetPaymentCommand(long PaymentId): IRequest<Object>;
}
