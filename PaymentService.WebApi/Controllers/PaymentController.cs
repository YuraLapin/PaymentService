using Mediator;
using Microsoft.AspNetCore.Mvc;
using PaymentService.DataAccess.Postgres.Models;
using PaymentService.WebApi.UseCases.Commands;

namespace PaymentService.Controllers
{
    // <summary>
    // Контроллер для адреса /payments
    // </summary>
    [Route("payments")]
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

        // <summary>
        // Записывает данные об оплате в БД
        // </summary>
        // <returns>
        // Id созданной записи
        // </returns>
        // <param name="payment">
        // Данные о добавляемой оплате
        // Принимается в теле запроса
        // </param>
        // <param name="ct">
        // Токен отмены
        // </param>
        [HttpPost("create")]
        public async Task<IActionResult> AddPayment([FromBody] WebApi.Models.Payment payment, CancellationToken ct)
        {
            var res = await _mediator.Send(new AddPaymentCommand(payment), ct);

            if (res is string)
            {
                return BadRequest(res);
            }

            return Json((long)res);
        }

        // <summary>
        // Обновляет данные о заданной записи об оплате в БД
        // </summary>
        // <param name="paymentId">
        // Id обновляемой записи об оплате
        // </param>
        // <param name="status">
        // Новое значение параметра status
        // </param>
        // <param name="ct">
        // Токен отмены
        // </param>
        [HttpPut("updateStatus/{paymentId:long}/{status:bool}")]
        public async Task<IActionResult> UpdatePayment(long paymentId, bool status, CancellationToken ct)
        {
            var res = await _mediator.Send(new UpdatePaymentCommand(paymentId, status), ct);

            if (res is string)
            {
                return BadRequest(res);
            }

            return Ok();
        }

        // <summary>
        // Получает запись об оплате из БД
        // </summary>
        // <param name="paymentId">
        // Id требуемой записи об оплате
        // </param>
        // <returns>
        // Требуемую запись об оплате
        // </returns>
        // <param name="ct">
        // Токен отмены
        // </param>
        [HttpGet("get/{paymentId:long}")]
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
