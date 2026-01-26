namespace PaymentService.DataAccess.Postgres.Models
{
    /// <summary>
    /// Модель записи об оплате, хранящейся в БД
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Идентификатор записи об оплате
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Сумма к оплате
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Статус оплаты (true - завершена)
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime DateCreated { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Payment payment)
            {
                return Id == payment.Id && OrderId == payment.OrderId && Price == payment.Price && Status == payment.Status;
            }

            return false;
        }
    }
}
