using Microsoft.EntityFrameworkCore;
using PaymentServiceDataBase.Models;

namespace PaymentServiceDataBase
{
    public class DataBaseContext: DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
