
/*
=========================================================
Journal Program - Exceeding Core Requirements (Report)
=========================================================

This program meets all the core requirements of the
Journal assignment and also includes several extra 
features to go beyond 93% and qualify for 100%.

---------------------------------------------------------
✅ CORE REQUIREMENTS MET (93%):
---------------------------------------------------------
• Write a new journal entry from a random prompt
• Display all journal entries
• Save the journal to a file
• Load the journal from a file
• Menu interface for user options
• At least 5 different prompts
• Uses multiple classes and demonstrates abstraction

---------------------------------------------------------
🌟 EXTRA FEATURES ADDED (for 100%):
---------------------------------------------------------
• Additional Entry Fields:
     - Each entry now stores a Title and Mood (1–5 rating)
       in addition to Date, Prompt, and Response

• JSON File Storage:
     - Journal entries are saved and loaded as JSON files
       (instead of simple text). This shows using a different
       data format and handling structured data.

• Abstraction and OOP Design:
     - The program is organized into three classes:
         • Entry   - represents a single journal entry
         • Journal - manages a list of entries and file I/O
         • JournalApp - handles user interface and menu logic
     - This shows the principle of abstraction by hiding 
       internal details inside methods and exposing a clean API.

• User Interface Enhancements:
     - Clears the screen between actions
     - Simple, clean menu navigation
     - Prompts are user-friendly to encourage journaling

=========================================================
This description explains what was done to exceed the
requirements as required by the assignment instructions.
=========================================================
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Entry
{
    public string Date { get; set; }
    public string Title { get; set; }
    public string Mood { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry(string prompt, string response, string title, string mood)
    {
        Date = DateTime.Now.ToString("yyyy-MM-dd");
        Title = title;
        Mood = mood;
        Prompt = prompt;
        Response = response;
    }
}

public class Journal
{
    public List<Entry> Entries { get; set; } = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        Entries.Add(entry);
    }

    public void SaveToFile(string filename)
    {
        string json = JsonSerializer.Serialize(Entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException();

        string json = File.ReadAllText(filename);
        Entries = JsonSerializer.Deserialize<List<Entry>>(json) ?? new List<Entry>();
    }

    public void Display()
    {
        if (Entries.Count == 0)
        {
            Console.WriteLine("No entries yet.");
            return;
        }

        foreach (var e in Entries)
        {
            Console.WriteLine($"{e.Date} ({e.Mood}/5) - {e.Title}");
            Console.WriteLine(e.Prompt);
            Console.WriteLine(e.Response);
            Console.WriteLine();
        }
    }
}

public class JournalApp
{
    private Journal journal = new Journal();
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What is something I accomplished today that I’m proud of?",
        "What is one thing I learned about myself today?"
    };

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== Journal Menu ====");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display all entries");
            Console.WriteLine("3. Save journal to a file");
            Console.WriteLine("4. Load journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("\nChoose an option (1-5): ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    SaveJournal();
                    break;
                case "4":
                    LoadJournal();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("\nInvalid choice. Press Enter to try again...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void WriteNewEntry()
    {
        Console.Clear();
        var rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];

        Console.WriteLine("📓 New Journal Entry");
        Console.WriteLine("-----------------------");
        Console.WriteLine($"Your prompt is:\n{prompt}");

        Console.Write("\nGive this entry a short title: ");
        string title = Console.ReadLine() ?? "";

        Console.Write("How are you feeling today (1–5)? ");
        string mood = Console.ReadLine() ?? "";

        Console.Write("\nYour response: ");
        string response = Console.ReadLine() ?? "";

        var entry = new Entry(prompt, response, title, mood);
        journal.AddEntry(entry);

        Console.WriteLine("\n✅ Your entry has been saved!");
        Pause();
    }

    private void DisplayJournal()
    {
        Console.Clear();
        Console.WriteLine("📖 All Journal Entries");
        Console.WriteLine("-----------------------");
        journal.Display();
        Pause();
    }

    private void SaveJournal()
    {
        Console.Clear();
        Console.Write("Enter a filename to save (e.g. myjournal.json): ");
        string filename = Console.ReadLine() ?? "";
        journal.SaveToFile(filename);
        Console.WriteLine($"\n✅ Journal saved to {filename}");
        Pause();
    }

    private void LoadJournal()
    {
        Console.Clear();
        Console.Write("Enter a filename to load: ");
        string filename = Console.ReadLine() ?? "";
        try
        {
            journal.LoadFromFile(filename);
            Console.WriteLine($"\n✅ Journal loaded from {filename}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("\nThat file does not exist.");
        }
        Pause();
    }

    private void Pause()
    {
        Console.WriteLine("\nPress Enter to return to the menu...");
        Console.ReadLine();
    }
}

public class Program
{
    public static void Main()
    {
        JournalApp app = new JournalApp();
        app.Run();
    }
}