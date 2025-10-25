namespace BankAccountApp.Models
{
    public abstract class AccountBase : IAccount
    {
        protected decimal balance;

        public AccountBase(decimal initialBalance)
        {
            balance = initialBalance;
        }

        public virtual void CheckBalance()
        {
            Console.WriteLine($"Your balance: {balance:N0} đ");
        }

        public virtual void Transfer(decimal amount)
        {
            if (amount > balance)
            {
                Console.WriteLine("Insufficient funds!");
                return;
            }

            balance -= amount;
            Console.WriteLine($"You transferred {amount:N0} đ, your new balance: {balance:N0} đ");
        }
    }
}
