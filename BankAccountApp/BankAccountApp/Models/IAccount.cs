namespace BankAccountApp.Models
{
    public interface IAccount
    {
        void CheckBalance();
        void Transfer(decimal amount);
    }
}
