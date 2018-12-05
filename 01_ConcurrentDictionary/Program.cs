using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _01_ConcurrentDictionary
{
    class Program
    {
        private static ConcurrentDictionary<string, string> _capitals =
            new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            bool success = _capitals.TryAdd("France", "Paris");
            string who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId) : "Main thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element");
        }
        static void Main(string[] args)
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();

            _capitals["Ukraine"] = "Boryspil";
            //_capitals["Ukraine"] = "Kyiv";
            _capitals.AddOrUpdate("Ukraine", "Kyiv", (k, old) => old + "--> Kyiv");

            Console.WriteLine(_capitals["Ukraine"]);


            _capitals["Sweden"] = "Uppsala";
            var capOfSweden = _capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine(capOfSweden);

            const string toRemove = "Sweden";
            string removed;
            var didRemove = _capitals.TryRemove(toRemove, out removed);

            if (didRemove)
            {
                Console.WriteLine($"We just removed {removed}");
            }
            else
            {
                Console.WriteLine($"Failed to remove the capital of {removed}");
            }

            //_capitals.Count is the expensive operation

            foreach (var kv in _capitals)
            {
                Console.WriteLine($" - {kv.Value} is the capital of {kv.Key}");
            }
        }
    }
}
