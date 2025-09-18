using System;
using System.Collections.Generic;
using System.IO;

using ScriptureMemorizer;

/*
=========================================================
Scripture Memorizer
=========================================================
This program helps users memorize scriptures by gradually
hiding random words until the entire passage is hidden.

---------------------------------------------------------
✅ CORE REQUIREMENTS MET:
---------------------------------------------------------
• Displays scripture reference and full text
• Clears the console and updates display each round
• Prompts the user to press Enter or type "quit"
• Hides a few random words at a time
• Stops when the user types "quit"
• Ends when all words are hidden
• Uses Encapsulation (Reference, Word, and Scripture classes)
• Supports both single verses (John 3:16)
  and verse ranges (Proverbs 3:5-6)

---------------------------------------------------------
🚀 EXCEEDING REQUIREMENTS (100%):
---------------------------------------------------------
• 📁 Scripture Library from File:
  - Scriptures are stored in an external file (scriptures.txt)
  - Each line has: Book|Chapter|Verse/Range|Text
  - Makes adding new scriptures simple (no code changes needed)

• 🎲 Random Scripture Selection:
  - On startup, the program chooses a random scripture from the file
  - Keeps practice fresh and unpredictable for the user

• 🧩 Object-Oriented and Extensible:
  - Proper encapsulation with private fields and public methods
  - Reference class has multiple constructors to handle single verses and verse ranges
  - Separation of responsibilities improves scalability and maintainability
---------------------------------------------------------
Author: [Your Name]
Date: [Date]
=========================================================
*/

namespace ScriptureMemorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Scripture> library = LoadScriptures("scriptures.txt");
            if (library.Count == 0)
            {
                Console.WriteLine("No scriptures found in scriptures.txt");
                return;
            }

            Random rnd = new Random();
            Scripture chosen = library[rnd.Next(library.Count)];

            while (!chosen.AllHidden())
            {
                Console.Clear();
                Console.WriteLine(chosen.GetReference());
                Console.WriteLine(chosen.GetDisplayText());

                Console.WriteLine("\nPress Enter to hide words or type 'quit' to stop:");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                    return;

                chosen.HideRandomWords(3);
            }

            // Final display
            Console.Clear();
            Console.WriteLine(chosen.GetReference());
            Console.WriteLine(chosen.GetDisplayText());
            Console.WriteLine("\n--- Scripture fully hidden ---");
        }

        static List<Scripture> LoadScriptures(string filename)
        {
            var scriptures = new List<Scripture>();
            if (!File.Exists(filename))
                return scriptures;

            foreach (var line in File.ReadAllLines(filename))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split('|');
                if (parts.Length < 4) continue;

                string book = parts[0];
                int chapter = int.Parse(parts[1]);
                string versePart = parts[2];
                string text = parts[3];

                Reference reference;
                if (versePart.Contains("-"))
                {
                    var verses = versePart.Split('-');
                    reference = new Reference(book, chapter, int.Parse(verses[0]), int.Parse(verses[1]));
                }
                else
                {
                    reference = new Reference(book, chapter, int.Parse(versePart));
                }

                scriptures.Add(new Scripture(reference, text));
            }
            return scriptures;
        }
    }
}