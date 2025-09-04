using System;

class Program
{
    static void Main(string[] args)
    {
        // Step 1: Display welcome message
        DisplayWelcome();

        // Step 2: Ask for user's name and save it
        string userName = PromptUserName();

        // Step 3: Ask for user's favorite number and save it
        int userNumber = PromptUserNumber();

        // Step 4: Square the number
        int squaredNumber = SquareNumber(userNumber);

        // Step 5: Display the result
        DisplayResult(userName, squaredNumber);
    }

    // Displays the message, "Welcome to the Program!"
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // Asks for and returns the user's name
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    // Asks for and returns the user's favorite number
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        return int.Parse(Console.ReadLine());
    }

    // Accepts an integer and returns that number squared
    static int SquareNumber(int number)
    {
        return number * number;
    }

    // Accepts the user's name and squared number and displays them
    static void DisplayResult(string name, int square)
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
    }
}