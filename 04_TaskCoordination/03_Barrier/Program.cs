using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _03_Barrier
{
    class Program
    {
        static Barrier barrier = new Barrier(2, b=>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
            
        });

        public static void Water()
        {
            Console.WriteLine("Putting the kettle on (takes a bit longer)");
            Thread.Sleep(2000);
            barrier.SignalAndWait();// _1_2
            Console.WriteLine("Pouring the water into the cup");// _2_0
            barrier.SignalAndWait();//_2_1
            Console.WriteLine("Putting the kettle on away");

        }

        public static void Cup()
        {
            Console.WriteLine("Finding the nicest cup of tea (fast)");
            barrier.SignalAndWait();// _1_1
            Console.WriteLine("Adding tea");
            barrier.SignalAndWait();//_2_2
            Console.WriteLine("Adding sugar");

        }
        static void Main(string[] args)
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Run(new Action(Cup));

            var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
             {
                 Console.WriteLine("Enjoy your cup of tea");
             });
            tea.Wait();
        }
    }
}
