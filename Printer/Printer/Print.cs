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
        static public int PaperCount { get; set; }
        static public bool IsFinished { get; set; }
        static int i = 0;
        static ConsoleKeyInfo k = default(ConsoleKeyInfo);
        static private int printCount;
        static public event MyDelegate OnStartPrinting = null;
        static public event EventDelegate OnOutOfPaper = null;
        static public event EventDelegate OnPause = null;
        static public void PressKeyPEvent()
        {
            
            OnPause();
           
        }
        static public void PressKeySEvent()
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
            if (PaperCount <= 0)
            if(k.Key == ConsoleKey.S)
            {
               Start(ref printCount);
            }
            if (k.Key == ConsoleKey.Escape)
            {
                IsFinished = true;
            }
        }
        static public void OutOfPaperEvent()
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
            Thread.Sleep(200);
            
        }
        static public void Start(ref int count)
        {
            //int printCount = count;
            while (k.Key != ConsoleKey.P && k.Key != ConsoleKey.Escape && PaperCount > 0 && count > 0 && IsFinished != true)
            {
                while (!Console.KeyAvailable && PaperCount > 0 && count > 0)
                {

                    Console.WriteLine("Press 'P' to pouse printing");
                    Printing(ref count);
                    if (Console.KeyAvailable)
                    {
                        switch (k.Key)
                        {
                            case ConsoleKey.S:
                                OnStartPrinting += Start;
                                PressKeySEvent();
                                break;
                            case ConsoleKey.P:
                                OnPause += Pouse;
                                PressKeyPEvent();
                                break;
                            case ConsoleKey.Escape:
                                IsFinished = true;
                                break;
                            default: continue;
                        }
                    }
                }
            }
        }
           
        

    }
}
