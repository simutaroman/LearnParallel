using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _05_Reader_Writer
{
    class Program
    {
        static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim();
        //static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim( LockRecursionPolicy.SupportsRecursion);
        static Random random = new Random();
        static void Main(string[] args)
        {
            int x = 0;

            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    padlock.EnterUpgradeableReadLock();
                    //padlock.EnterReadLock();
                    //padlock.EnterReadLock();

                    if (i % 2 == 0)
                    {
                        padlock.EnterWriteLock();
                        x = 123;
                        padlock.ExitWriteLock();
                    }
                    else
                    {
                        padlock.EnterWriteLock();
                        x = 0;
                        padlock.ExitWriteLock();
                    }
                    Console.WriteLine($"Entered read lock, x= {x}");
                    Thread.Sleep(5000);
                    padlock.ExitUpgradeableReadLock();
                    //padlock.ExitReadLock();
                    ////padlock.ExitReadLock();
                    Console.WriteLine($"Exited read lock, x= {x}");
                }));
            }   

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
                catch(AggregateException ae)
            {
                ae.Handle((e) =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }

            while (true)
            {
                Console.ReadKey();
                padlock.EnterWriteLock();
                Console.WriteLine("Write lock acquired");

                int newValue = random.Next(10);
                x = newValue;
                Console.WriteLine($"Set x = {x}");
                padlock.ExitWriteLock();
                Console.WriteLine("Write lock released");


            }
        }
    }
}
