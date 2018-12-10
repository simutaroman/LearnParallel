using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _04_Mutex3
{
    class Program
    {
        static void Main(string[] args)
        {
            const string appName = "MyApp";
            Mutex mutex;

            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorrry. the {appName} is already running");

            }
            catch (WaitHandleCannotBeOpenedException ex)
            {
                Console.WriteLine("We can run the program just fine");
                mutex = new Mutex(false, appName);

            }

            Console.ReadKey();
            mutex.ReleaseMutex();
        }
    }
}
