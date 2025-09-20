using System;
using System.Collections.Generic;
using System.IO;

public class ScriptureLibrary
{
    private readonly List<Scripture> _scriptures = new List<Scripture>();
    private static readonly Random rand = new Random();

    public ScriptureLibrary(string filePath)
    {
        LoadFromFile(filePath);
    }

    private void LoadFromFile(string filePath)
    {
        // Always load relative to the build folder (bin/Debug/net8.0/)
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

        if (!File.Exists(fullPath))
        {
            Console.WriteLine($"File not found: {fullPath}");
            return;
        }

        string[] lines = File.ReadAllLines(fullPath);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Expected format: Reference|Text
            string[] parts = line.Split('|');
            if (parts.Length == 2)
            {
                try
                {
                    Reference reference = ParseReference(parts[0].Trim());
                    string text = parts[1].Trim();
                    _scriptures.Add(new Scripture(reference, text));
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Skipping invalid reference '{parts[0]}': {ex.Message}");
                }
            }
        }
    }

    private Reference ParseReference(string refString)
    {
        // Example inputs: "John 3:16", "Proverbs 3:5-6", "2 Timothy 1:7"

        // Find the last space: everything before is the book, after is chapter:verse
        int lastSpaceIndex = refString.LastIndexOf(' ');
        if (lastSpaceIndex == -1)
            throw new FormatException($"Invalid reference format: {refString}");

        string book = refString.Substring(0, lastSpaceIndex); // handles multi-word book names
        string chapterAndVerse = refString.Substring(lastSpaceIndex + 1);

        string[] chapterVerseParts = chapterAndVerse.Split(':');
        if (chapterVerseParts.Length != 2)
            throw new FormatException($"Invalid chapter/verse format: {refString}");

        int chapter = int.Parse(chapterVerseParts[0]);
        string versePart = chapterVerseParts[1];

        if (versePart.Contains('-'))
        {
            string[] verseRange = versePart.Split('-');
            int startVerse = int.Parse(verseRange[0]);
            int endVerse = int.Parse(verseRange[1]);
            return new Reference(book, chapter, startVerse, endVerse);
        }
        else
        {
            int verse = int.Parse(versePart);
            return new Reference(book, chapter, verse);
        }
    }

    // Nullable return type to avoid CS8600 warnings
    public Scripture? GetRandomScripture()
    {
        if (_scriptures.Count == 0)
            return null;

        int index = rand.Next(_scriptures.Count);
        return _scriptures[index];
    }
}