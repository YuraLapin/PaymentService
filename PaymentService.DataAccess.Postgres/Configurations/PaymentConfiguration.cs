using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.DataAccess.Postgres.Models;

namespace PaymentService.DataAccess.Postgres.Configurations
{
    /// <summary>
    /// Настройки полей БД
    /// </summary>
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(k => k.Id);
        }
    }
}
