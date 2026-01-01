using System;
namespace Bank {
    //Creating our own Exception Class by inheriting Exception class
    public class OddNumberException : Exception
    {
        //Overriding the Message property
        public override string Message
        {
            get
            {
                return "Divisor Cannot be Odd Number";
            }
        }

        //Overriding the HelpLink Property
        public override string HelpLink
        {
            get
            {
                return "Get More Information from here: https://dotnettutorials.net/lesson/create-custom-exception-csharp/";
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int Number1, Number2, Result;
            try
            {
                Console.WriteLine("Enter First Number:");
                Number1 = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Second Number:");
                Number2 = int.Parse(Console.ReadLine());

                if (Number2 % 2 > 0)
                {
                    //OddNumberException ONE = new OddNumberException();
                    //throw ONE;
                    throw new OddNumberException();
                }
                Result = Number1 / Number2;
                Console.WriteLine(Result);
            }
            catch (OddNumberException one)
            {
                Console.WriteLine($"Message: {one.Message}");
                Console.WriteLine($"HelpLink: {one.HelpLink}");
                Console.WriteLine($"Source: {one.Source}");
                Console.WriteLine($"StackTrace: {one.StackTrace}");
            }

            Console.WriteLine("End of the Program");
            Console.ReadKey();
        }
    }
}