using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private readonly Reference _reference;
    private readonly List<Word> _words;
    private static readonly Random rand = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void HideRandomWords(int numberToHide)
    {
        for (int i = 0; i < numberToHide; i++)
        {
            int index = rand.Next(_words.Count);
            _words[index].Hide();
        }
    }

    public void RevealOneWord()
    {
        var hiddenWords = _words.Where(w => w.IsHidden()).ToList();
        if (hiddenWords.Count > 0)
        {
            int index = rand.Next(hiddenWords.Count);
            hiddenWords[index].Show();
        }
    }

    public string GetDisplayText()
    {
        return $"{_reference.GetDisplayText()} - " +
               string.Join(" ", _words.Select(w => w.GetDisplayText()));
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }
}