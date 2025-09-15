using Microsoft.EntityFrameworkCore;
using PaymentService.DataAccess.Postgres.Models;

namespace PaymentService.DataAccess.Postgres
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
