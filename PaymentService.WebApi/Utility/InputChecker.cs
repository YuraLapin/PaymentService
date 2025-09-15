using PaymentService.WebApi.Models;

namespace PaymentService.WebApi.Utility
{
    public class InputChecker
    {
        public string? CheckPayment(Payment payment)
        {
            return null;
        }

        public string? CheckOrderId(long id)
        {
            if (id < 0) return "Id заказа не может быть меньше нуля";
            return null;
        }
    }
}
