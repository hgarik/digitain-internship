using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printer1
{
    public class Printer
    {
        public event EventHandler PrintStarted;
        public event EventHandler PrintFinished;
        public event EventHandler OutOfPaper;
        public event EventHandler<PrintInProgressEventArgs> PrintInProgress;

        public Printer(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public int PapersCount { get; private set; }

        public void AddPapers(int count)
        {
            PapersCount += count;
        }
        protected virtual void OnOutOfPaper()
        {
            OutOfPaper?.Invoke(this, EventArgs.Empty);
        }
        public void Print(int pagesCount)
        {
            OnPrintStarted();

            for (int i = 0; i < pagesCount; i++)
            {
                if (PapersCount == 0)
                {
                    OnOutOfPaper();
                }

                PapersCount--;
                OnPrintInProgress(i + 1, pagesCount);
            }

            OnPrintFinished();
        }

        protected virtual void OnPrintStarted()
        {
            PrintStarted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPrintFinished()
        {
            PrintFinished?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPrintInProgress(int printedCount, int total)
        {
            var e = new PrintInProgressEventArgs
            {
                PrintedPagesCount = printedCount,
                PagesCount = total
            };

            PrintInProgress?.Invoke(this, e);
        }
    }
}
