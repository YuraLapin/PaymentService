using PaymentService.DataAccess.Postgres.Models;

namespace PaymentService.DataAccess.Postgres
{
    public class DataBaseService
    {
        private readonly DataBaseContext _db;

        public DataBaseService(DataBaseContext db)
        {
            _db = db;
        }

        public async Task<long> AddPayment(long orderId, CancellationToken ct)
        {
            var newPayment = new Payment { OrderId = orderId, Status = false };
            _db.Payments.Add(newPayment);
            await _db.SaveChangesAsync(ct);

            if (ct.IsCancellationRequested)
            {
                _db.Payments.Remove(newPayment);
            }

            return newPayment.Id;
        }

        public Payment? GetPayment(long orderId)
        {
            Payment? res = _db.Payments.Find(new Payment() { OrderId = orderId });
            return res;
        }

        public async Task DeletePayment(long id)
        {
            _db.Payments.Remove(new Payment() { Id = id });
            await _db.SaveChangesAsync();
        }
    }
}
