using System;

class Program
{
    static void Main(string[] args)
    {
        // Load scriptures from file
        ScriptureLibrary library = new ScriptureLibrary("scriptures.txt");

        // Nullable because GetRandomScripture() can return null
        Scripture? scripture = library.GetRandomScripture();

        if (scripture == null)
        {
            Console.WriteLine("No scripture could be loaded. Please check scriptures.txt.");
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words, type 'reveal' to reveal one, or type 'quit' to exit.");
            string? input = Console.ReadLine();

            if (input == "quit")
                break;
            else if (input == "reveal")
                scripture.RevealOneWord();
            else
                scripture.HideRandomWords(3);

            if (scripture.IsCompletelyHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nAll words are hidden. Program will end.");
                break;
            }
        }
    }
}

/*
=========================================================
Report: Exceeding Requirements
=========================================================
1. Supports a library of scriptures stored in a file (scriptures.txt).
2. Random scripture is chosen each run.
3. User can reveal a hidden word with the "reveal" command.
4. Handles verse ranges like Proverbs 3:5-6.
5. Uses encapsulation and OOP design with 5 classes:
   - Program
   - Scripture
   - Reference
   - Word
   - ScriptureLibrary
6. Clean error handling when no scriptures are loaded.
=========================================================
*/