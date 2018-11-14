using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_CreateStartTasks
{
    class Program
    {
        public static void Write(char c)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(c);
            }
        }

        public static void Write(object o)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }

        public static int TextLenth(object o)
        {
            Console.WriteLine($"\nTask with id={Task.CurrentId} is processing object {o}");
            return o.ToString().Length;
        }

        static void Main(string[] args)
        {
            //Task.Factory.StartNew(() => Write('.'));
            //var t = new Task(() => Write('?'));
            //t.Start();
            //Write('!');

            //Task t2 = new Task(Write, "Hello");
            //t2.Start();
            //Task.Factory.StartNew(Write, 1234);

            string text1 = "testing", text2 = "this";
            var task1 = new Task<int>(TextLenth, text1);
            task1.Start();

            Task<int>task2 = Task.Factory.StartNew(TextLenth, text2);

            Console.WriteLine($"The length of the {text1} is {task1.Result}");
            Console.WriteLine($"The length of the {text2} is {task2.Result}");

            Console.WriteLine("Main thread done");
            Console.ReadKey();
        }

        
    }
}

