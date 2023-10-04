using System;
using System.Threading;

// A class to represent a chopstick
class Chopstick
{
    // A flag to indicate if the chopstick is available
    private bool available = true;

    // A method to get the availability of the chopstick
    public bool IsAvailable()
    {
        return available;
    }

    // A method to set the availability of the chopstick
    public void SetAvailable(bool value)
    {
        available = value;
    }
}

// A class to represent a philosopher
class Philosopher
{
    // The name of the philosopher
    private string name;

    // The left and right chopstick
    private Chopstick left;
    private Chopstick right;

    // A random number generator
    private Random random = new Random();

    // A constructor that takes the name and the chopsticks as parameters
    public Philosopher(string name, Chopstick left, Chopstick right)
    {
        this.name = name;
        this.left = left;
        this.right = right;
    }

    // A method to get the name of the philosopher
    public string GetName()
    {
        return name;
    }

    // A method that simulates eating or sleeping
    public void EatOrSleep()
    {
        // Try to acquire the left chopstick
        if (Monitor.TryEnter(left))
        {
            // If successful, try to acquire the right chopstick
            if (Monitor.TryEnter(right))
            {
                // If successful, set both chopsticks as unavailable
                left.SetAvailable(false);
                right.SetAvailable(false);

                // Display that the philosopher is eating
                Console.WriteLine(name + " is eating.");

                // Simulate eating for a random amount of time
                Thread.Sleep(random.Next(1000, 5000));

                // Release both chopsticks
                Monitor.Exit(right);
                Monitor.Exit(left);

                // Set both chopsticks as available
                left.SetAvailable(true);
                right.SetAvailable(true);
            }
            else
            {
                // If not successful, release the left chopstick
                Monitor.Exit(left);
            }
        }

        // Display that the philosopher is sleeping
        Console.WriteLine(name + " is sleeping.");

        // Simulate sleeping for a random amount of time
        Thread.Sleep(random.Next(1000, 5000));
    }
}

// A class to represent a table with 5 philosophers and 5 chopsticks
class Table
{
    // An array of 5 philosophers
    private Philosopher[] philosophers = new Philosopher[5];

    // An array of 5 chopsticks
    private Chopstick[] chopsticks = new Chopstick[5];

    // A constructor that creates and initializes the philosophers and the chopsticks
    public Table()
    {
        // Create 5 chopsticks and set them as available
        for (int i = 0; i < 5; i++)
        {
            chopsticks[i] = new Chopstick();
            chopsticks[i].SetAvailable(true);
        }

        // Create 5 philosophers and assign them their left and right chopstick
        philosophers[0] = new Philosopher("Aristotle", chopsticks[4], chopsticks[0]);
        philosophers[1] = new Philosopher("Plato", chopsticks[0], chopsticks[1]);
        philosophers[2] = new Philosopher("Socrates", chopsticks[1], chopsticks[2]);
        philosophers[3] = new Philosopher("Descartes", chopsticks[2], chopsticks[3]);
        philosophers[4] = new Philosopher("Kant", chopsticks[3], chopsticks[4]);
    }

    // A method that starts a thread for each philosopher and displays their state periodically
    public void Start()
    {
        // Start a thread for each philosopher
        for (int i = 0; i < 5; i++)
        {
            int index = i; // To avoid closure issues
            Thread thread = new Thread(() =>
            {
                // Loop indefinitely
                while (true)
                {
                    // Call the EatOrSleep method of the philosopher
                    philosophers[index].EatOrSleep();
                }
            });

            // Set the thread as background
            thread.IsBackground = true;

            // Start the thread
            thread.Start();
        }

        // Loop indefinitely
        while (true)
        {
            // Display the state of each philosopher and chopstick
            Console.WriteLine("Philosopher\tState\t\tChopstick");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(philosophers[i].GetName() + "\t\t" + (chopsticks[i].IsAvailable() ? "Sleeping" : "Eating") + "\t\t" + (chopsticks[i].IsAvailable() ? "Available" : "In use"));
            }

            // Add a blank line
            Console.WriteLine();

            // Wait for 1 second
            Thread.Sleep(1000);
        }
    }
}

// A class to represent the console app
class Program
{
    // The main method
    static void Main()
    {
        // Create an instance of the table class
        Table table = new Table();

        // Start the table
        table.Start();
    }
}
