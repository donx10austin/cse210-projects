using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your first name? ");
        string first = Console.ReadLine();

        Console.Write("What is your last name? ");
        string last = Console.ReadLine();

        // The corrected line using string.Format()
        Console.WriteLine(string.Format("Your name is {0}, {1} {0}.", last, first));
    }
}