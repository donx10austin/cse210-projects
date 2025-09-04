using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string answer = Console.ReadLine();

        try
        {
            double percent = double.Parse(answer);

            if (percent < 0 || percent > 100)
            {
                Console.WriteLine("Please enter a grade between 0 and 100.");
                return;
            }

            string letter = "";
            string sign = "";

            if (percent >= 90)
            {
                letter = "A";
            }
            else if (percent >= 80)
            {
                letter = "B";
            }
            else if (percent >= 70)
            {
                letter = "C";
            }
            else if (percent >= 60)
            {
                letter = "D";
            }
            else
            {
                letter = "F";
            }

            int lastDigit = (int)percent % 10;
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
            
            if (letter == "A" && sign == "+")
            {
                sign = "";
            }
            if (letter == "F")
            {
                sign = "";
            }

            
            Console.WriteLine($"Your grade is: {letter}{sign}");

            if (percent >= 70)
            {
                Console.WriteLine("Congratulations, you passed the course!");
            }
            else
            {
                Console.WriteLine("Better luck next time!");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}



