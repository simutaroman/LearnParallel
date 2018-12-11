using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _05_ManualResetEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            var evt = new ManualResetEventSlim();

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set();
            });

            var maketea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water");
                evt.Wait();
                Console.WriteLine("Here is your tea");
                evt.Wait();// signal is set, so the next string will be executed
                Console.WriteLine("The end");
            });
            maketea.Wait();
        }
    }
}
