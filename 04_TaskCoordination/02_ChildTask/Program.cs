using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _02_ChildTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var parent = new Task(() =>
            {
                //detached
                var child = new Task(()=>
                {
                    Console.WriteLine("Child task starting;");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child task finishing");
                    throw new Exception();
                }, TaskCreationOptions.AttachedToParent);
                child.Start();
                var completionHandler =
                    child.ContinueWith(t => { Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}"); },
                        TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);
                var failHandler =
                    child.ContinueWith(t => { Console.WriteLine($"Oops, task {t.Id}'s state is {t.Status}"); },
                        TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

            });
            

            parent.Start();
            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle((e=>true));
                //throw;
            }
        }
    }
}
