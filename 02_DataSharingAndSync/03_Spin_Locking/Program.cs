using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _03_Spin_Locking
{
    public class BankAccount
    {
        private int balance;

        public int Balance
        {
            get
            {
                return balance;
            }
            private set
            {
                balance = value;
            }
        }
        public void Deposit(int amount)
        {
            Balance += amount;

        }
        public void Withdraw(int amount)
        {
            Balance -= amount;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            SpinLock sl = new SpinLock();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        var lockTacken = false;
                        try
                        {
                            sl.Enter(ref lockTacken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (lockTacken) sl.Exit();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        var lockTacken = false;
                        try
                        {
                            sl.Enter(ref lockTacken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTacken) sl.Exit();
                        }
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance {ba.Balance}");
        }
    }
}
