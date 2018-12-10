using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ParallelInvokeForForEach
{
    class Program
    {
        public static IEnumerable<int> Range (int start, int end, int step)
        {
            for (int i = start; i < end; i+=step)
            {
                yield return i;
            }
        }

        static void Main(string[] args)
        {
            //int[] values = new int[100];
            //for (int i = 0; i < 100; i++)
            //{

            //}

            var a = new Action(() => Console.WriteLine($"First {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"Second {Task.CurrentId}"));
            var c = new Action(() => Console.WriteLine($"Third {Task.CurrentId}"));
            Parallel.Invoke(a, b, c);

            Parallel.For(1, 11, i =>
            {
                Console.Write($"{i * i}\t");
            });
            Console.WriteLine();

            string[] words = { "one", "two", "three", "four" };

            Parallel.ForEach(words, word =>
            {
                Console.WriteLine($"{word} has length{word.Length} (task {Task.CurrentId})");
            });



            Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);
        }
    }
}
