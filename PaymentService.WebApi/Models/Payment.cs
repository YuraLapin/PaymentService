namespace PaymentService.WebApi.Models
{
    /// <summary>
    /// Модель записи об оплате,
    /// приходящая в запросе к сервису
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Сумма к оплате
        /// </summary>
        public decimal Price { get; set; }
    }
}
