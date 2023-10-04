using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PhilosophiesTable
{
    internal class Philosophers
    {
        private string name;

        private ChopSticks left;
        private ChopSticks right;

        private Random random = new Random();

        public Philosophers (string name, ChopSticks left, ChopSticks right)
        {
            this.name = name;
            this.left = left;
            this.right = right;
        }
        public string GetName() 
        {
            return name;
        }

        public void EatOrSleep() 
        {
            //private readonly object lockObj = new object();
            //lock(lockObj)
            {
            if (Monitor.TryEnter(left))
                {
                if (Monitor.TryEnter(right)) 
                {
                    left.SetAvailable(false);
                    right.SetAvailable(true);

                    Console.WriteLine($"{name} is eating.");

                    //Thread.Sleep(random.Next(1000, 5000));
                    Thread.Sleep(5000);

                    Monitor.Exit(right);
                    Monitor.Exit(left);

                    left.SetAvailable(true);
                    right.SetAvailable(true);
                }
                else 
                {
                    Monitor.Exit(left);
                }
            
            }
            Console.WriteLine($"{name} is sleeping");

            //Thread.Sleep(random.Next(1000, 5000));
            Thread.Sleep(5000);
            }
        }
    }
}
