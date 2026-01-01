//using System;
//namespace ControlFlowDemo


//{
//    public class Bank
//    {
//        public long AccountNumber;
//        public string Name;
//        public int Balance;

//        public Bank(long _accountNumber, string _name, int _balance)
//        {
//            AccountNumber = _accountNumber;
//            Name = _name;
//            Balance = _balance;
//        }

//        public void Getbalance()
//        {
//            Console.WriteLine($"Account Number: {AccountNumber}, Name: {Name}, Balance: {Balance}");
//        }

//        public int  WithdrawAmount(int deduction) 
//        {
//            return Balance -= deduction;
//        }

//        public int Deposit(int MoneyDeposited)
//        {
//            return  Balance += MoneyDeposited;
//        }
//    }
//    class Program
//    {
//        static void Main(string[] args)
//        {

//            Bank equity = new Bank(1234567890, "John Doe", 5000);
//            equity.Getbalance();
//            equity.Deposit(2000);
//            equity.Getbalance();
//            equity.WithdrawAmount(1500);
//            equity.Getbalance();
//            Console.ReadKey();
//        }
//    }
//}

using System;
namespace HelloWorld
{
    public class Bank
    {
        private double _Amount;
        public double Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                // Validate the value before storing it in the _Amount variable
                if (value < 0)
                {
                    throw new Exception("Please Pass a Positive Value");
                }
                else
                {
                    _Amount = value;
                }
            }
        }
    }
    class Program
    {
        public static void Main()
        {
            try
            {
                Bank bank = new Bank();
                //We cannot access the _Amount Variable directly
                //bank._Amount = 50; //Compile Time Error
                //Console.WriteLine(bank._Amount); //Compile Time Error

                //Setting Positive Value using public Amount Property
                bank.Amount = 10;

                //Setting the Value using public Amount Property
                Console.WriteLine(bank.Amount);

                //Setting Negative Value
                bank.Amount = -150;
                Console.WriteLine(bank.Amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}