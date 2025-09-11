using Microsoft.AspNetCore.Mvc;
using PaymentServiceDataBase;
using PaymentServiceDataBase.Models;
using OrderServiceMain.Refit;
using OrderServiceMain.Utility;

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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("payments")]
        public async Task<IActionResult> AddPayment(int orderId, CancellationToken ct)
        {
            //_logger.LogWarning($"Order sum{sum} name{clientName}");

            string? errorMessage = _inputChecker.CheckOrderId(orderId);
            if (errorMessage != null) return BadRequest(errorMessage);

            var newId = await _dbService.AddPayment(orderId, ct);

            if (ct.IsCancellationRequested)
            {
                await _dbService.DeletePayment(newId);
                return StatusCode(499);
            }

            return Ok();
        }

        [HttpGet("payments/{id:int}")]
        public async Task<IActionResult> GetPayment(int orderId, CancellationToken ct)
        {
            if (ct.IsCancellationRequested) return StatusCode(499);

            string? errorMessage = _inputChecker.CheckOrderId(orderId);
            if (errorMessage != null) return BadRequest(errorMessage);

            Payment? res = _dbService.GetPayment(orderId);
            if (res == null)
            {
                //_logger.LogWarning($"No order { id }");
                return Json(false);
            }

            //_logger.LogWarning($"Order { id }");
            return Json(res.IsComplete);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return Error();
        //}
    }
}
