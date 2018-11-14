using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _02_CancelTask
{
    class Program
    {
        static void Main(string[] args)
        {
            //var cts = new CancellationTokenSource();
            //var token = cts.Token;
            //token.Register(() =>
            //{
            //    Console.WriteLine("Cancellation has been requested");
            //});
            //var t = new Task(() =>
            //{
            //    var i = 0;
            //    while (true)
            //    {
            //        //canonical exit 2 (3)
            //        token.ThrowIfCancellationRequested();
            //        //if (token.IsCancellationRequested)
            //        //{
            //        //    //release resources, if any
            //        //    //break;//soft exit (1)
            //        //    throw new OperationCanceledException();//canonical exit 1. exception is not propagated (2)
            //        //}

            //        Console.Write($"{i++}\t");
            //    }
            //}, token);
            //t.Start();

            //Task.Factory.StartNew(() =>{
            //    token.WaitHandle.WaitOne();
            //    Console.WriteLine("Wait handle released. Cancellation was requested");
            //});

            //Console.ReadKey();
            //cts.Cancel();


            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            planned.Token.Register(() =>
            {
                Console.WriteLine("planned cancellation..."); 
            });
            preventative.Token.Register(() =>
            {
                Console.WriteLine("preventative cancellation...");
            });
            emergency.Token.Register(() =>
            {
                Console.WriteLine("emergency cancellation...");
            });

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preventative.Token, emergency.Token);

            Task.Factory.StartNew(() =>
            {
                var i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.Write($"{i++}\t");
                    Thread.Sleep(1000);
                }
            }, paranoid.Token);

            Console.ReadKey();
            // any of tokens works
            planned.Cancel();
            //emergency.Cancel();
            //preventative.Cancel();

            Console.WriteLine("Main thread done");
            //Console.ReadKey();
        }
    }
}
