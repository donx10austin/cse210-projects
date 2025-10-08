using System;
using System.Collections.Generic;
using System.IO;

/*
============================================================
Eternal Quest Program
Author: [Your Name]
Date: [Submission Date]

DESCRIPTION:
This program tracks various kinds of goals using object-oriented
principles — including inheritance, polymorphism, and encapsulation.
It allows the user to create, save, load, and record progress
on different types of goals, while earning points.

============================================================
EXCEEDS REQUIREMENTS:
- Added a **Leveling System** where users level up every 1000 points.
- Added **Progress Display** for checklist goals.
- Added **Dynamic Motivational Messages** when completing goals.
- Clean, user-friendly interface and modular design.
============================================================
*/

// -------------------- BASE CLASS --------------------
abstract class Goal
{
    // Encapsulation: private/protected fields
    protected string _shortName;
    protected string _description;
    protected int _points;

    // Constructor
    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    // Abstract methods — must be implemented by derived classes
    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStringRepresentation();

    // Virtual method (can be overridden)
    public virtual string GetDetailsString()
    {
        return $"{_shortName} ({_description})";
    }

    // Accessor method
    public int GetPoints() => _points;
}

// -------------------- SIMPLE GOAL --------------------
class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    public override void RecordEvent()
    {
        _isComplete = true;
        Console.WriteLine($"✅ You completed '{_shortName}' and earned {_points} points!");
    }

    public override bool IsComplete() => _isComplete;

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{_shortName},{_description},{_points},{_isComplete}";
    }
}

// -------------------- ETERNAL GOAL --------------------
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        Console.WriteLine($"♾ You recorded progress on '{_shortName}' and earned {_points} points!");
    }

    public override bool IsComplete() => false;

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{_shortName},{_description},{_points}";
    }
}

// -------------------- CHECKLIST GOAL --------------------
class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = 0;
    }

    public override void RecordEvent()
    {
        _amountCompleted++;
        Console.WriteLine($"📋 Progress recorded for '{_shortName}' ({_amountCompleted}/{_target})!");
        Console.WriteLine($"+{_points} points earned!");

        if (IsComplete())
        {
            Console.WriteLine($"🎉 Goal complete! Bonus +{_bonus} points awarded!");
        }
    }

    public override bool IsComplete() => _amountCompleted >= _target;

    public override string GetDetailsString()
    {
        return $"{_shortName} ({_description}) -- Completed: {_amountCompleted}/{_target}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{_shortName},{_description},{_points},{_target},{_bonus},{_amountCompleted}";
    }

    // Helper to get bonus value
    public int GetBonus() => _bonus;
    public int GetAmountCompleted() => _amountCompleted;
    public int GetTarget() => _target;
}

// -------------------- GOAL MANAGER --------------------
class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;
    private int _level = 1;

    public void Start()
    {
        int choice;
        do
        {
            Console.WriteLine("\n====================================");
            Console.WriteLine("        🧭 Eternal Quest Menu");
            Console.WriteLine("====================================");
            Console.WriteLine($"🏅 Score: {_score} | Level: {_level}");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Select an option: ");

            // Input handling
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number 1–6.");
                continue;
            }

            switch (choice)
            {
                case 1: CreateGoal(); break;
                case 2: ListGoalDetails(); break;
                case 3: SaveGoals(); break;
                case 4: LoadGoals(); break;
                case 5: RecordEvent(); break;
                case 6: Console.WriteLine("Goodbye, adventurer! 👋"); break;
                default: Console.WriteLine("Invalid choice."); break;
            }

        } while (choice != 6);
    }

    // -------- CREATE GOAL --------
    public void CreateGoal()
    {
        Console.WriteLine("\nSelect goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        int type = int.Parse(Console.ReadLine());

        Console.Write("Enter short name: ");
        string name = Console.ReadLine();
        Console.Write("Enter description: ");
        string desc = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case 1:
                _goals.Add(new SimpleGoal(name, desc, points));
                break;
            case 2:
                _goals.Add(new EternalGoal(name, desc, points));
                break;
            case 3:
                Console.Write("Enter target count: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
                break;
        }

        Console.WriteLine("🌟 Goal created successfully!");
    }

    // -------- LIST GOALS --------
    public void ListGoalDetails()
    {
        Console.WriteLine("\nYour Goals:");
        if (_goals.Count == 0)
        {
            Console.WriteLine("(No goals yet!)");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Goal g = _goals[i];
            string check = g.IsComplete() ? "[X]" : "[ ]";
            Console.WriteLine($"{i + 1}. {check} {g.GetDetailsString()}");
        }
    }

    // -------- RECORD EVENT --------
    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals to record yet!");
            return;
        }

        ListGoalDetails();
        Console.Write("Enter goal number to record: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index < 0 || index >= _goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            return;
        }

        Goal goal = _goals[index];
        goal.RecordEvent();

        // Add points
        _score += goal.GetPoints();

        // Bonus for checklist goals
        if (goal is ChecklistGoal checklist && checklist.IsComplete())
        {
            _score += checklist.GetBonus();
        }

        // Level up every 1000 points
        if (_score >= _level * 1000)
        {
            _level++;
            Console.WriteLine($"🎯 You leveled up! Welcome to Level {_level}!");
        }

        // Random motivation message
        string[] messages = { "Keep going!", "You're unstoppable!", "Nice work!", "Epic progress!" };
        Console.WriteLine(messages[new Random().Next(messages.Length)]);
    }

    // -------- SAVE GOALS --------
    public void SaveGoals()
    {
        Console.Write("Enter filename to save: ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);
            outputFile.WriteLine(_level);
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
        Console.WriteLine("💾 Goals saved successfully!");
    }

    // -------- LOAD GOALS --------
    public void LoadGoals()
    {
        Console.Write("Enter filename to load: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        _score = int.Parse(lines[0]);
        _level = int.Parse(lines[1]);
        _goals.Clear();

        for (int i = 2; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(':');
            string type = parts[0];
            string[] data = parts[1].Split(',');

            switch (type)
            {
                case "SimpleGoal":
                    var simple = new SimpleGoal(data[0], data[1], int.Parse(data[2]));
                    if (bool.Parse(data[3])) simple.RecordEvent();
                    _goals.Add(simple);
                    break;

                case "EternalGoal":
                    _goals.Add(new EternalGoal(data[0], data[1], int.Parse(data[2])));
                    break;

                case "ChecklistGoal":
                    var checklist = new ChecklistGoal(data[0], data[1], int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[4]));
                    _goals.Add(checklist);
                    break;
            }
        }

        Console.WriteLine("📂 Goals loaded successfully!");
    }
}

// -------------------- MAIN PROGRAM --------------------
class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}


