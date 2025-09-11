namespace OrderServiceMain.Utility
{
    public class InputChecker
    {

        public string? CheckOrderId(int id)
        {
            if (id < 0) return "Id заказа не может быть меньше нуля";
            return null;
        }
    }
}
