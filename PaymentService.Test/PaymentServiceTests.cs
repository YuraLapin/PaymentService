using Microsoft.AspNetCore.Mvc.Testing;
using PaymentService.Test.Refit;
using PaymentService.WebApi;
using Refit;
using Testcontainers.PostgreSql;

namespace PaymentService.Test
{
    /// <summary>
    /// Тесты для сервиса оплаты
    /// </summary>
    [TestFixture]
    public class PaymentServiceTests
    {
        private readonly PostgreSqlContainer _paymentDbContainer = new PostgreSqlBuilder().Build();
        private WebApplicationFactory<Program> _webApplicationFactory;
        private IPaymentApi _paymentApi;

        /// <summary>
        /// Разворачивание контейнеров с необходимыми сервисами
        /// </summary>
        [OneTimeSetUp]
        public async Task Setup()
        {
            await _paymentDbContainer.StartAsync();
            _webApplicationFactory = new CustomWebApplicationFactory(_paymentDbContainer.GetConnectionString());

            HttpClient httpClient = _webApplicationFactory.CreateClient();
            _paymentApi = RestService.For<IPaymentApi>(httpClient);
        }

        /// <summary>
        /// Тесты для создания записи об оплате
        /// </summary>
        [Test]
        [TestCase(1, 2.0, "OK")]
        [TestCase(-1, 2.0, "BadRequest")]
        [TestCase(1, -2.0, "BadRequest")]
        public async Task AddPaymentTest(long orderId, decimal price, string expected)
        {
            var newPayment = new WebApi.Models.Payment()
            {
                OrderId = orderId,
                Price = price,
            };

            ApiResponse<long> response = await _paymentApi.AddPayment(newPayment);
            string actual = response.StatusCode.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Тесты создания, а затем получения созданной записи об оплате
        /// </summary>
        [Test]
        [TestCase(10, 5.0)]
        [TestCase(902, 125.23210)]
        public async Task GetPaymentTest(long orderId, decimal price)
        {
            var newPayment = new WebApi.Models.Payment()
            {
                OrderId = orderId,
                Price = price,
            };

            ApiResponse<long> addPaymentRes = await _paymentApi.AddPayment(newPayment);
            long addedId = addPaymentRes.Content;

            var expected = new DataAccess.Postgres.Models.Payment()
            {
                Id = addedId,
                OrderId = orderId,
                Price = price,
                Status = false,
            };

            ApiResponse<DataAccess.Postgres.Models.Payment> getPaymentRes = await _paymentApi.GetPayment(addedId);
            DataAccess.Postgres.Models.Payment actual = getPaymentRes.Content;

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Тесты получения несуществующей записи об оплате
        /// </summary>
        [Test]
        [TestCase(-1)]
        [TestCase(92929)]
        public async Task GetWrongPaymentTest(long paymentId)
        {
            string expected = "BadRequest";

            ApiResponse<DataAccess.Postgres.Models.Payment> res = await _paymentApi.GetPayment(paymentId);
            string actual = res.StatusCode.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Тесты создания, изменения и получения записи об оплате
        /// </summary>
        [Test]
        [TestCase(10, 5.0, true)]
        [TestCase(902, 125.23210, false)]
        public async Task UpdatePaymentTest(long orderId, decimal price, bool status)
        {
            // Добавление записи
            var newPayment = new WebApi.Models.Payment()
            {
                OrderId = orderId,
                Price = price,
            };

            ApiResponse<long> addPaymentRes = await _paymentApi.AddPayment(newPayment);
            long addedId = addPaymentRes.Content;

            var expected = new DataAccess.Postgres.Models.Payment()
            {
                Id = addedId,
                OrderId = orderId,
                Price = price,
                Status = status,
            };

            // Обновление записи
            await _paymentApi.UpdatePayment(addedId, status);

            // Получение записи
            ApiResponse<DataAccess.Postgres.Models.Payment> getPaymentRes = await _paymentApi.GetPayment(addedId);
            DataAccess.Postgres.Models.Payment actual = getPaymentRes.Content;

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Тесты изменения не существующей записи об оплате
        /// </summary>
        [Test]
        [TestCase(-1, true)]
        [TestCase(92929, false)]
        public async Task UpdateWrongPaymentTest(long paymentId, bool status)
        {
            string expected = "BadRequest";

            ApiResponse<string> res = await _paymentApi.UpdatePayment(paymentId, status);
            string actual = res.StatusCode.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Сворачивание контейнеров
        /// </summary>
        [OneTimeTearDown]
        public async Task Dispose()
        {
            await _webApplicationFactory.DisposeAsync();
            await _paymentDbContainer.DisposeAsync();
        }
    }
}
