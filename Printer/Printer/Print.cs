using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Printer
{
    public delegate void EventDelegate();
    public delegate void MyDelegate(ref int a);
    public class Print
    {
        static private int paperCount;
        //static public int PaperCount { get; set; }
        static public bool IsFinished { get; set; }

        static public int PaperCount
        {
            get
            {
                return paperCount;
            }

            set
            {
                if (value > 0 && value <= 200)
                {
                    paperCount = value;
                }
                else
                    Console.WriteLine("incorect Value");
            }
        }

        static int i = 0;
        static ConsoleKeyInfo k = default(ConsoleKeyInfo);
        static private int printCount;
        static public event MyDelegate OnStartPrinting = null;
        static public event MyDelegate OnPrinting = null;
        static public event EventDelegate OnOutOfPaper = null;
        static public event EventDelegate OnPause = null;
        static public void PressKeyPhundler()
        {
            OnPause();
        }
        static public void PressKeySHundler(ref int printCount)
        {
            OnStartPrinting.Invoke(ref printCount);
        }
        public Print(int printCount, int count = 20)
        {
            Print.printCount = printCount;
            PaperCount = count;
        }
        static public void OutOfPaper()
        {
            Console.WriteLine("printer is out of paper");
            Console.WriteLine("enter the number of paper you want to insert or press Esc key to finish printing");
            if (k.Key != ConsoleKey.Escape)
            {
                do
                {
                    try
                    {
                        PaperCount = int.Parse(Console.ReadLine());

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Enter the count of paper you want to insert(Min 1 and Max 200)");
                        PaperCount = 0;

                    }
                } while (printCount == 0);
                PaperCount = int.Parse(Console.ReadLine());


                if (!IsFinished)
                Start(ref printCount);
            }
            else
            {
                IsFinished = true;
            }
        }
        static public void Pouse()
        {
            Console.WriteLine("Paused");
            Console.WriteLine("Press 'S' to Start or Escape to Finish printing");
            k = Console.ReadKey();
            if(k.Key == ConsoleKey.S)
            {
               Start(ref printCount);
            }
            if (k.Key == ConsoleKey.Escape)
            {
                IsFinished = true;
            }
        }
        static public void PrintingHundler(ref int printCount)
        {
            OnPrinting(ref printCount);
        }
        static public void OutOfPaperHundler()
        {
            OnOutOfPaper();
        }
        static public void Printing(ref int count)
        {
            i++;
            PaperCount--;
            count--;
            Console.Clear();
            Console.WriteLine($"printing {i} page");
            Console.WriteLine("Press 'P' to pouse printing");
            Thread.Sleep(200);
            
        }
        static public void Start(ref int count)
        {
            while (PaperCount > 0 && printCount > 0 && IsFinished != true)
            {
                if(OnPrinting==null)
                {
                   OnPrinting += Printing;
                }
                PrintingHundler(ref printCount);
                if (Console.KeyAvailable)
                {
                    k = Console.ReadKey();
                    switch (k.Key)
                    {
                        case ConsoleKey.P:
                            OnPause += Pouse;
                            PressKeyPhundler();
                            break;
                        case ConsoleKey.Escape:
                            IsFinished = true;
                            Console.WriteLine("interapt by user");
                            break;
                        default: continue;
                    }
                }
            }
            if (PaperCount <= 0)
            {
                OnOutOfPaper += OutOfPaper;
                OutOfPaperHundler();
            }
            if (printCount <=0)
            {
                IsFinished = true;
            }
        }



    }
}
