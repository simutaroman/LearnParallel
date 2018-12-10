using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _03_LockRecursion
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
        static SpinLock sl = new SpinLock(true);
        public static void LockRecursion(int x)
        {
            bool lockTaken = false;

            try
            {
                sl.Enter(ref lockTaken);
            }
            catch (LockRecursionException ex)
            {

                Console.WriteLine($"Exception: {ex}");
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, x={x}");
                    LockRecursion(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x={x}");
                }
            }
        }

        static void Main(string[] args)
        {
            LockRecursion(5);
        }
    }
}
