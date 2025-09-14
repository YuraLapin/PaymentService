using PaymentServiceDataBase.Models;

namespace PaymentServiceDataBase
{
    public class DataBaseService
    {
        private readonly DataBaseContext _db;

        public DataBaseService(DataBaseContext db)
        {
            _db = db;
        }

        public async Task<int> AddPayment(int orderId, CancellationToken ct)
        {
            var newPayment = new Payment { OrderId = orderId, IsComplete = false };
            _db.Payments.Add(newPayment);
            await _db.SaveChangesAsync(ct);

            if (ct.IsCancellationRequested)
            {
                _db.Payments.Remove(newPayment);
            }

            return newPayment.Id;
        }

        public Payment? GetPayment(int orderId)
        {
            Payment? res = _db.Payments.Find(new Payment() { OrderId = orderId });
            return res;
        }

        public async Task DeletePayment(int id)
        {
            _db.Payments.Remove(new Payment() { Id = id });
            await _db.SaveChangesAsync();
        }
    }
}
