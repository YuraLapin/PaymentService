using Mediator;
using Microsoft.AspNetCore.Mvc;
using PaymentService.DataAccess.Postgres.Models;
using PaymentService.WebApi.UseCases.Commands;

namespace PaymentService.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IMediator _mediator;

        public PaymentController
        (
            ILogger<PaymentController> logger,
            IMediator mediator
        )
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("payments/create")]
        public async Task<IActionResult> AddPayment(WebApi.Models.Payment payment, CancellationToken ct)
        {
            var res = await _mediator.Send(new AddPaymentCommand(payment), ct);

            if (res is string)
            {
                return BadRequest(res);
            }

            return Json((long)res);
        }

        [HttpPut("payments/updateStatus/{paymentId:long}/{status:bool}")]
        public async Task<IActionResult> UpdatePayment(long paymentId, bool status, CancellationToken ct)
        {
            var res = await _mediator.Send(new UpdatePaymentCommand(paymentId, status), ct);

            if (res is string)
            {
                return BadRequest(res);
            }

            return Ok();
        }

        [HttpGet("payments/get/{paymentId:long}")]
        public async Task<IActionResult> GetPayment(long paymentId, CancellationToken ct)
        {
            var res = await _mediator.Send(new GetPaymentCommand(paymentId), ct);

            if (res is string)
            {
                return BadRequest(res);
            }

            return Json((Payment)res);
        }
    }
}
