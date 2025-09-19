using System.ComponentModel.DataAnnotations;

namespace PaymentService.DataAccess.Postgres.Models
{
    // <summary>
    // Модель записи об оплате, хранящейся в БД
    // </summary>
    public class Payment
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
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
