using Mediator;
using PaymentService.WebApi.Models;

namespace PaymentService.WebApi.UseCases.Commands
{
    public sealed record class AddPaymentCommand(Payment Payment) : IRequest<Object>;
}
