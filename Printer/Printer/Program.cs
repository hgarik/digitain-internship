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
        //static ConsoleKeyInfo k = default(ConsoleKeyInfo);

        static int printCount;
        static void Main(string[] args)
        {
            Console.WriteLine("There is 20 Papers in the printer");
            Console.WriteLine("Enter the cout of pages you want to print");
            printCount = int.Parse(Console.ReadLine());
            //Console.WriteLine("Press 'S' key to start printing");
            Print p = new Print(printCount);

            Print.Start(ref printCount);
            if (printCount <= 0)
            {
                Console.WriteLine("Finished");
                Print.IsFinished = true;
            }
            else if (Print.PaperCount <= 0)
            {
                Print.OnOutOfPaper += Print.OutOfPaper;
                Print.OutOfPaperEvent();
            }
            if (Print.IsFinished)
            {
                Console.WriteLine("Thank you for using our printer");
            }


            Console.ReadKey();
        }
    }
}
