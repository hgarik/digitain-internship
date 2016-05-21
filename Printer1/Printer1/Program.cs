using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Printer1
{
    class Program
    {
        static void Main(string[] args)
        {
            var HP = new Printer("HP LaserJet");
            HP.OutOfPaper += (sender, e) =>
            {
                Console.WriteLine($"{(sender as Printer).Name} is out of Paper \nInsert count of paper you want to add");
                (sender as Printer).AddPapers(int.Parse(Console.ReadLine()));
            };
            HP.PrintStarted += (sender, e) => {
                Thread.Sleep(1000);
                Console.WriteLine($"{(sender as Printer)?.Name}:: Print Started"); };
            HP.PrintFinished += (sender, e) => {
                Thread.Sleep(1000);
                Console.WriteLine($"{(sender as Printer)?.Name}:: Print Finished"); };
            HP.PrintInProgress += (sender, e) => {
                Thread.Sleep(1000);
                Console.WriteLine($"{(sender as Printer)?.Name}:: Printing {e.PrintedPagesCount} of {e.PagesCount} [{e.Progress}%]");
            };

            HP.AddPapers(2);

            HP.Print(4);
        }
    }
}