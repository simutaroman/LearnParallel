using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingForTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                Console.WriteLine("I take 5 seconds");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("I'm done in 5 seconds");
            }, token);
            t.Start();
            //t.Wait(token);

            Task t2 = Task.Factory.StartNew(() => {
                Thread.Sleep(3000);
                Console.WriteLine("I'm done in 3 seconds");
            }, token);


            //Console.ReadKey();
            //cts.Cancel();

            //Task.WaitAll(t, t2);
            //Task.WaitAny(t, t2);
            //Task.WaitAny(new[] {t, t2}, 4000, token);
            Task.WaitAll(new[] { t, t2 }, 4000, token);

            Console.WriteLine($"Task t status is {t.Status}");
            Console.WriteLine($"Task t2 status is {t2.Status}");

            Console.WriteLine("Main thread done!");
            Console.ReadKey();
        }
    }
}
