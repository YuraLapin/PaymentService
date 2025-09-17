using Microsoft.EntityFrameworkCore;
using PaymentService.DataAccess.Postgres.Models;
using PaymentService.DataAccess.Postgres.Configurations;

namespace PaymentService.DataAccess.Postgres
{
    // <summary>
    // Контекст базы данных
    // </summary>
    public class DataBaseContext: DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        }
    }
}
