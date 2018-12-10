using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_ExceptionHandling
{
    class Program
    {
        static void Main2(string[] args)
        {
            try
            {
                Test();
            }
            catch (AggregateException ae)
            {

                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Handles elsewhere {e.GetType()} from {e.Source}");
                }
            }
            Console.WriteLine("Main thread done");
            Console.ReadKey();
        }

        private static void Test()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this") { Source = "t" };
            });
            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t, t2);
            }
            catch (AggregateException ae)
            {
                //foreach (var e in ae.InnerExceptions)
                //{
                //    Console.WriteLine($"Exception {e.GetType()} from {e.Source}");
                //}

                ae.Handle(e =>
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid ops");
                        return true; 
                    }
                    return false;
                });
            }
        }
    }
}
