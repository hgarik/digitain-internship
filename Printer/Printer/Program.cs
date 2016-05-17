using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;



namespace Printer
{
    class Program
    {
        //static int i = 0;
        static ConsoleKeyInfo k = default(ConsoleKeyInfo);
        public const int maxCount = 200;
        static private int printCount = 0;

        public static int PrintCount
        {
            get
            {
                return printCount;
            }

            set
            {
                if(value > 0 && value <= maxCount)
                {
                     printCount = value;
                }
                else
                    Console.WriteLine("incorect Value");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("There is 20 Papers in the printer");
            Console.WriteLine("Enter the count of pages you want to print(Min 1 and Max 200)");
            do
            {
                try
                {
                    PrintCount = int.Parse(Console.ReadLine());

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Enter the count of pages you want to print(Min 1 and Max 200)");
                    printCount = 0;
                    
                }
            } while (printCount == 0);
            Console.WriteLine("Press 'S' key to start printing");
            Print p = new Print(printCount);
            k = Console.ReadKey();
            if (k.Key == ConsoleKey.S)
            {
                Print.OnStartPrinting += Print.Start;
                Print.PressKeySHundler(ref printCount);
            }
            if (printCount <= 0)
            {
                Console.WriteLine("Finished");
                Print.IsFinished = true;
            }
            if (Print.IsFinished)
            {
                Console.WriteLine("Thank you for using our printer");
            }
            


            Console.ReadKey();
        }
    }
}
