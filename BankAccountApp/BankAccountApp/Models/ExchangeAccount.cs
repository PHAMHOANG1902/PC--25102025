namespace BankAccountApp.Models
{
    public class ExchangeAccount : AccountBase
    {
        private decimal exchangeRate; // e.g. 25,000
        private decimal amountInForeign; // e.g. 1,000 USD

        public ExchangeAccount(decimal rate, decimal foreignAmount) 
            : base(rate * foreignAmount)
        {
            exchangeRate = rate;
            amountInForeign = foreignAmount;
        }

        public override void CheckBalance()
        {
            Console.WriteLine($"Exchange Rate: {exchangeRate:N0}");
            Console.WriteLine($"Your balance: {balance:N0} đ");
        }
    }
}
