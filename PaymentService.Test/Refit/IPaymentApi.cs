using Refit;

namespace PaymentService.Test.Refit
{
    public interface IPaymentApi
    {
        [Post("/payments/create")]
        Task<ApiResponse<long>> AddPayment(WebApi.Models.Payment payment);

        [Get("/payments/get/{paymentId}")]
        Task<ApiResponse<DataAccess.Postgres.Models.Payment>> GetPayment(long paymentId);

        [Put("/payments/updateStatus/{paymentId}/{newStatus}")]
        Task<ApiResponse<string>> UpdatePayment(long paymentId, bool newStatus);
    }
}
