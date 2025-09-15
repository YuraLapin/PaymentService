using System.ComponentModel.DataAnnotations;

namespace PaymentService.DataAccess.Postgres.Models
{
    public class Payment
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; }
        public bool Status { get; set; }
    }
}
