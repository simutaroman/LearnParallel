using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _05_AutoResetEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            var evt = new AutoResetEvent(false);//1false

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set();//3true
            });

            var maketea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water");
                evt.WaitOne();// 2false 4false
                Console.WriteLine("Here is your tea");
                //evt.WaitOne();// will hang forever
                //evt.Set();//to get ok, you have to set signal again
                var ok = evt.WaitOne(1000);
                if (ok)
                {
                    Console.WriteLine("Enjoy");
                }
                else
                {
                    Console.WriteLine("No tea for you");
                }
                
            });
            maketea.Wait();
        }
    }
}
