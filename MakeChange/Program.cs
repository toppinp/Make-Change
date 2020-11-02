using System;
using System.Security.Cryptography.X509Certificates;

namespace MakeChange
{
    class Program
    {
        static void Main(string[] args)
        {
            double payment;
            double purchase;

            purchase = GetPurchaseAmount();

            payment = GetPaymentAmount();

            double changeDue = CalculateChange(payment, purchase);

            computeChangeDenoms(changeDue);
        }

        public static double GetPurchaseAmount()
        {
            Console.Write("Purchase Amount: $");
            string userInput = Console.ReadLine();
            double price = double.Parse(userInput);
            while (price <= 0)
            {
                Console.WriteLine("Invalid purchase amount.  Please try again.");
                userInput = Console.ReadLine();
                price = double.Parse(userInput);
            }
            return price;
        }

        public static double GetPaymentAmount()
        {
            Console.Write("Payment Amount: $");
            string userInput = Console.ReadLine();
            double payment = double.Parse(userInput);
            while (payment <= 0)
            {
                Console.WriteLine("Invalid payment amount.  Please try again.");
                userInput = Console.ReadLine();
                payment = double.Parse(userInput);
            }

            return payment;
        }

        public static double CalculateChange(double Payment, double Purchase)
        {
            double changeDue = Payment - Purchase;
            changeDue += 0.001d; // Add 1/10th of a penny to compensate for rounding error
            testExactChange(Payment, Purchase);
            while (Purchase > Payment)
            {
                Console.WriteLine("You have not rendered the proper amount.  Please try again.");
                Purchase = GetPurchaseAmount();
                Payment = GetPaymentAmount();
                testExactChange(Payment, Purchase);
                changeDue = Payment - Purchase;
            }

            Console.WriteLine($" Change: {changeDue:C2}");
            Console.WriteLine();
            return changeDue;
        }
        static double GetDisplayDenomination(double changeDue, int billValue, string billName)
        {
            
            int change = (int)(changeDue / billValue);
            if (change != 0)
            {
                Console.WriteLine($"Give back {change} {billName}.");
                changeDue -= (change * billValue);
            }
            return changeDue;
        }

        static void testExactChange(double Payment, double Purchase)
        {
            if (Payment - Purchase == 0)
            {
                Console.WriteLine("You have made an exact payment with no change due.");
                Environment.Exit(1);
            }

        }

        static void computeChangeDenoms(double changeDue)
        {
            changeDue = GetDisplayDenomination(changeDue, 20, "twenties");

            changeDue = GetDisplayDenomination(changeDue, 10, "tens");

            changeDue = GetDisplayDenomination(changeDue, 5, "fives");

            changeDue = GetDisplayDenomination(changeDue, 1, "ones");

            changeDue *= 100;

            changeDue = GetDisplayDenomination(changeDue, 25, "quarters");

            changeDue = GetDisplayDenomination(changeDue, 10, "dimes");

            changeDue = GetDisplayDenomination(changeDue, 5, "nickels");

            GetDisplayDenomination(changeDue, 1, "pennies");

        }
    }
}
