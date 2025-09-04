using System;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for grade percentage
        Console.Write("Enter your grade percentage: ");
        string input = Console.ReadLine();
        int percent = int.Parse(input);

        string letter = "";
        string sign = "";

        // Determine the letter grade
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

        // Determine the sign (+ or -)
        int lastDigit = percent % 10;

        if (letter == "A")
        {
            // Only A or A- (no A+)
            if (lastDigit < 3)
            {
                sign = "-";
            }
        }
        else if (letter == "F")
        {
            // F never has + or -
            sign = "";
        }
        else // B, C, D grades
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        // Display the grade with the sign
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // Check if the user passed the course
        if (percent >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Better luck next time! Keep trying and don’t give up.");
        }
    }
}