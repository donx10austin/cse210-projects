using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        int userNumber = -1;

        
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        // Collect numbers until user enters 0
        while (userNumber != 0)
        {
            Console.Write("Enter number: ");
            string userResponse = Console.ReadLine();

            if (int.TryParse(userResponse, out userNumber))
            {
                if (userNumber != 0)
                {
                    numbers.Add(userNumber);
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid integer.");
            }
        }

        if (numbers.Count > 0)
        {
            int sum = 0;
            foreach (int n in numbers)
            {
                sum += n;
            }
            Console.WriteLine($"The sum is: {sum}");

            float average = (float)sum / numbers.Count;
            Console.WriteLine($"The average is: {average}");

            int max = numbers[0];
            foreach (int n in numbers)
            {
                if (n > max)
                {
                    max = n;
                }
            }
            Console.WriteLine($"The largest number is: {max}");

            int? smallestPositive = null;
            foreach (int n in numbers)
            {
                if (n > 0)
                {
                    if (smallestPositive == null || n < smallestPositive)
                    {
                        smallestPositive = n;
                    }
                }
            }

            if (smallestPositive != null)
            {
                Console.WriteLine($"The smallest positive number is: {smallestPositive}");
            }
            else
            {
                Console.WriteLine("No positive numbers were entered.");
            }

            numbers.Sort();
            Console.WriteLine("The sorted list is:");
            foreach (int n in numbers)
            {
                Console.WriteLine(n);
            }
        }
        else
        {
            Console.WriteLine("No numbers were entered.");
        }
    }
}