using System.ComponentModel.DataAnnotations;

namespace PaymentServiceDataBase.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public bool IsComplete { get; set; }
    }
}
