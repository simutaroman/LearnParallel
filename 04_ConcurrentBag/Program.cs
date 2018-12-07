using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace _04_ConcurrentBag
{
    class Program
    {
        static void Main(string[] args)
        {
            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var i1 = i;
                tasks.Add(Task.Run(() =>
                {
                    bag.Add(i1);
                    Console.WriteLine($"{Task.CurrentId} has added {i1}");
                    int result;
                    if (bag.TryPeek(out result))
                    {
                        Console.WriteLine($"{Task.CurrentId} has peeked out {result}");
                    }
                }));

            }

            Task.WaitAll(tasks.ToArray());

            int last = -1;
            if (bag.TryTake(out last))
            {
                Console.WriteLine($"I got {last}");
            }
        }
    }
}
