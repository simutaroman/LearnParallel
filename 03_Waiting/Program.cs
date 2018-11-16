using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _03_Waiting
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                // unlike sleep and waitone
                // thread does not give up its turn
                // avoiding a context switch
                //Thread.Sleep(1000);//(1)
                //Thread.SpinWait(10); //(2)
                //SpinWait.SpinUntil(() => false);// (3)

                Console.WriteLine("Press any key to disarm. You have 5 seconds...");
                bool cancelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled ? "Disarmed" : "BOOM");
                
            }, token);
            t.Start();

            // unlike sleep and waitone
            // thread does not give up its turn
            // avoiding a context switch
            Thread.SpinWait(1000);
            
            SpinWait.SpinUntil(() => true); //if false - it's getting unresponsive
            Console.WriteLine("Are you still here?");

            Console.ReadKey();
            cts.Cancel();
            Console.WriteLine("Main program done");
            Console.ReadKey();
        }
    }
}
