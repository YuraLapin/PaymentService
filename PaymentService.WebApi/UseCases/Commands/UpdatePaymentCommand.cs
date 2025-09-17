using Mediator;

namespace PaymentService.WebApi.UseCases.Commands
{
    // <summary>
    // Mediator команда для обновления записи об оплате из БД
    // </summary>
    // <param name="PaymentId">
    // Id обновляемой записи
    // </param>
    // <param name="Status">
    // Новое значение параметра status записи
    // </param>
    public sealed record class UpdatePaymentCommand(long PaymentId, bool Status) : IRequest<string?>;
}
