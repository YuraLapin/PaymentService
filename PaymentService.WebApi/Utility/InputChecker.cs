using PaymentService.WebApi.Models;

namespace PaymentService.WebApi.Utility
{
    public class InputChecker
    {
        public string? CheckPayment(Payment payment)
        {
            string? idError = CheckId(payment.OrderId);
            if (idError != null) return idError;

            string? priceError = CheckPrice(payment.Price);
            if (priceError != null) return priceError;

            return null;
        }

        public string? CheckId(long id)
        {
            if (id < 0) return "Id не может быть меньше нуля";
            return null;
        }

        private string? CheckPrice(decimal price)
        {
            if (price < 0) return "Сумма оплаты не может быть отрицательным числом";
            return null;
        }
    }
}
