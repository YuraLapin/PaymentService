using Microsoft.AspNetCore.Mvc;
using PaymentService.DataAccess.Postgres;
using PaymentService.DataAccess.Postgres.Models;
using PaymentService.WebApi.Models;
using PaymentService.WebApi.Utility;

namespace PaymentService.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DataBaseService _dbService;
        private readonly InputChecker _inputChecker;

        public PaymentController
        (
            ILogger<PaymentController> logger,
            DataBaseService dbService,
            IConfiguration configuration,
            InputChecker inputChecker
        )
        {
            _logger = logger;
            _configuration = configuration;
            _dbService = dbService;
            _inputChecker = inputChecker;
        }

        [HttpPost("payments/create")]
        public async Task<IActionResult> AddPayment(WebApi.Models.Payment payment, CancellationToken ct)
        {
            string? errorMessage = _inputChecker.CheckPayment(payment);
            if (errorMessage != null) return BadRequest(errorMessage);

            var newId = await _dbService.AddPayment(payment.OrderId, ct);

            if (ct.IsCancellationRequested)
            {
                await _dbService.DeletePayment(newId);
                return StatusCode(499);
            }

            return Ok();
        }

        [HttpGet("payments/{id:int}")]
        public async Task<IActionResult> GetPayment(long orderId, CancellationToken ct)
        {
            if (ct.IsCancellationRequested) return StatusCode(499);

            string? errorMessage = _inputChecker.CheckOrderId(orderId);
            if (errorMessage != null) return BadRequest(errorMessage);

            DataAccess.Postgres.Models.Payment? res = _dbService.GetPayment(orderId);
            if (res == null)
            {
                return Json(false);
            }

            return Json(res.Status);
        }
    }
}
