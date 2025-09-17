namespace PaymentService.WebApi.Models
{
    // <summary>
    // Модель записи об оплате,
    // приходящая в запросе к сервису
    // </summary>
    public class Payment
    {
        public long OrderId { get; set; }
        public decimal Price { get; set; }
    }
}
