using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _02_Breaking
{
    class Program
    {
        private static ParallelLoopResult result;
        static void Demo()
        {
            var cts = new CancellationTokenSource();
            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;

            result = Parallel.For(0, 20, po, (x, state) =>
            {
                Console.WriteLine($"{x}[{Task.CurrentId}]");
                if (x == 10)
                {
                    //state.Stop();//ASAP
                    //state.Break();// Less immediate than the Stop
                    //throw new Exception();
                    cts.Cancel();
                }
            });

            Console.WriteLine();
            Console.WriteLine($"Was loop completed? {result.IsCompleted}");
            if (result.LowestBreakIteration.HasValue)
            {
                Console.WriteLine($"lowest break iteration is {result.LowestBreakIteration}");
            }
        }
        static void Main(string[] args)
        {
            try
            {
                Demo();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }
            catch (OperationCanceledException oce)
            {
                Console.WriteLine(oce.Message);
            }
            
        }
    }
}
