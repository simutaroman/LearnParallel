using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _03_ThreadLocalStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            //Parallel.For(1, 1001, x =>
            //{
            //    Interlocked.Add(ref sum, x);// too much of interlocked
            //});

            
            // calculate partial sum in thread local storage
            // then, calculate common sum
            Parallel.For(1, 1001,
                () => 0,
                (x, state, tls) =>
                {
                    tls += x;
                    Console.WriteLine($"Task {Task.CurrentId} has partial sum {tls}");
                    return tls;
                }, 
                partialSum =>
                {
                    Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                    Interlocked.Add(ref sum, partialSum);
                });

            Console.WriteLine(sum);

        }
    }
}
