using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilosophiesTable
{
    public class Table
    {
        private Philosophers[] philosophers = new Philosophers[5];

        private ChopSticks[] chopsticks = new ChopSticks[5];

        public Table() 
        {
            for (int i = 0; i < 5; i++) 
            {
                chopsticks[i] = new ChopSticks();
                chopsticks[i].SetAvailable(true);
            }

            philosophers[0] = new Philosophers("Aristotle", chopsticks[4], chopsticks[0]);
            philosophers[1] = new Philosophers("Plato", chopsticks[0], chopsticks[1]);
            philosophers[2] = new Philosophers("Socrates", chopsticks[2], chopsticks[1]);
            philosophers[3] = new Philosophers("Descrates", chopsticks[2], chopsticks[3]);
            philosophers[4] = new Philosophers("Kant", chopsticks[3], chopsticks[4]);

        }

        public void start() 
        {
            for (int i=0; i < 5; i++) 
            {
                int index = i;
                Thread thread = new Thread(() =>
                {
                    while (true)
                    {
                        philosophers[index].EatOrSleep();
                    }
                });

                thread.IsBackground = true;
                thread.Start();


            }

            while (true) 
            {
                Console.WriteLine("Philosopher\tState\t\tChopstick");
                
                for (int i =0; i < 5; i++) 
                {
                    Console.WriteLine(philosophers[i].GetName() + "\t\t" + (chopsticks[i].IsAvailable() ? "Sleeping" : "Eating") +
                        "\t\t" + (chopsticks[i].IsAvailable() ? "Available" : "In Use"));
                }
                Console.WriteLine();

                Thread.Sleep(5000);
            }
        
        }
    }

}
