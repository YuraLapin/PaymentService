using Mediator;
using PaymentService.DataAccess.Postgres.Models;

namespace PaymentService.WebApi.UseCases.Commands
{
    public sealed record class GetPaymentCommand(long PaymentId): IRequest<Object>;
}
