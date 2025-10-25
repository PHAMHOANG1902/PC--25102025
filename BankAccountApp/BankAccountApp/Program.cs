using BankAccountApp.Models;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== BANK ACCOUNT SYSTEM ===");
        Console.WriteLine("1. Normal Account");
        Console.WriteLine("2. Exchange Account");
        Console.Write("Select account type: ");
        int choice = int.Parse(Console.ReadLine()!);

        IAccount account;

        if (choice == 1)
        {
            Console.Write("Enter your initial balance (VND): ");
            decimal amount = decimal.Parse(Console.ReadLine()!);
            account = new NormalAccount(amount);
        }
        else
        {
            Console.Write("Enter exchange rate (e.g. 25000): ");
            decimal rate = decimal.Parse(Console.ReadLine()!);
            Console.Write("Enter your amount in foreign currency: ");
            decimal foreign = decimal.Parse(Console.ReadLine()!);
            account = new ExchangeAccount(rate, foreign);
        }

        Console.WriteLine();
        account.CheckBalance();

        Console.Write("\nEnter amount to transfer: ");
        decimal transferAmount = decimal.Parse(Console.ReadLine()!);
        account.Transfer(transferAmount);

        Console.WriteLine("\n=== END ===");
    }
}
