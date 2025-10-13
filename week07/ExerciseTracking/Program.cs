using System;
using System.Collections.Generic;

// ----------------------------------------------------
// 1. Demonstration (Top-level statements must precede type declarations)
// ----------------------------------------------------

// Create instances of the derived classes
List<Activity> activities = new List<Activity>
{
    // Running: 40 min, 5.0 km
    new Running("2024-10-12", 40.0, 5.0),
    
    // Cycling: 60 min (1 hour), 20 kph
    new Cycling("2024-10-13", 60.0, 20.0),
    
    // Swimming: 30 min, 40 laps (40 * 0.05 = 2.0 km)
    new Swimming("2024-10-14", 30.0, 40),

    // Another Running: 25 min, 3.5 km
    new Running("2024-10-15", 25.0, 3.5),
    
    // Test case: Zero duration (handles division by zero)
    new Running("2024-10-16", 0.0, 2.0),
};

Console.WriteLine("--- Fitness Activity Tracker ---");
Console.WriteLine("Program Demonstrating Inheritance, Polymorphism, and Encapsulation.");
Console.WriteLine("Units: Kilometers (km) | Time: Minutes (min) / Hours (h)\n");
Console.WriteLine("Tracking Log:");
Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");

// Iterate through the list, demonstrating polymorphism.
foreach (Activity activity in activities)
{
    Console.WriteLine(activity.GetSummary());
}

Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
Console.WriteLine("\nDemonstration of Individual Calculations (Polymorphism):");

// Accessing the polymorphic properties directly:
Console.WriteLine($"1. Running Speed (Calculated):   {activities[0].SpeedKph:0.2} kph");
Console.WriteLine($"2. Cycling Distance (Calculated): {activities[1].DistanceKm:0.2} km");
Console.WriteLine($"3. Swimming Pace (Calculated):   {activities[2].PaceMinKm:0.2} min/km");
Console.WriteLine($"4. Zero Duration Speed:          {activities[5].SpeedKph:0.2} kph (Should be 0.00)");
Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");

// ----------------------------------------------------
// 2. Class Definitions (Now follow the top-level statements)
// ----------------------------------------------------

public abstract class Activity
{
    // Fix CS0122: Changed from private to protected so derived classes can access them.
    protected DateTime _date;
    protected double _durationMin;
    
    // Moved LapPoolLengthKm here and made it protected so Swimming can use it.
    protected const double LapPoolLengthKm = 0.05;

    /// <summary>
    /// Initializes a new instance of the Activity class.
    /// </summary>
    /// <param name="dateStr">The date of the activity in YYYY-MM-DD format.</param>
    /// <param name="durationMin">The length of the activity in minutes.</param>
    public Activity(string dateStr, double durationMin)
    {
        // All member variables are now protected, allowing access in derived classes
        try
        {
            // Convert date string to a DateTime object
            _date = DateTime.Parse(dateStr);
        }
        catch (FormatException)
        {
            Console.WriteLine($"Error: Invalid date format provided: {dateStr}. Expected YYYY-MM-DD.");
            _date = DateTime.Today;
        }
        
        _durationMin = durationMin;
    }

    // --- Abstract Properties (Polymorphism & Method Overriding) ---
    // Subclasses MUST implement these properties.

    public abstract double DistanceKm { get; }

    public abstract double SpeedKph { get; }

    public abstract double PaceMinKm { get; }
    
    // --- Shared Method (Available to all derived classes via Inheritance) ---

    public string GetSummary()
    {
        // Format the date as '12 Oct 2024'
        string dateFmt = _date.ToString("dd MMM yyyy");
        
        // Determine the activity name (e.g., 'Running') using reflection
        string activityName = this.GetType().Name;

        return (
            $"{dateFmt} {activityName} ({_durationMin:0} min): " +
            $"Distance {DistanceKm:0.1} km, " +
            $"Speed: {SpeedKph:0.1} kph, " +
            $"Pace: {PaceMinKm:0.1} min per km"
        );
    }
}

// ----------------------------------------------------
// 3. Derived Classes (Demonstrates Inheritance and specific implementation)
// ----------------------------------------------------

public class Running : Activity
{
    // Specific private data
    private double _distance; 

    /// <summary>
    /// Initializes a new instance of the Running activity.
    /// </summary>
    public Running(string dateStr, double durationMin, double distanceKm) : base(dateStr, durationMin) // Inherits shared attributes
    {
        _distance = distanceKm; // Stored specific private data
    }

    public override double DistanceKm => _distance; // Method overriding: Returns the stored distance.

    public override double SpeedKph 
    {
        get
        {
            // Accessing protected member _durationMin (fixed CS0122)
            if (_durationMin == 0) return 0.0;
            double timeHours = _durationMin / 60.0;
            return _distance / timeHours;
        }
    }

    public override double PaceMinKm 
    {
        get
        {
            // Accessing protected member _durationMin (fixed CS0122)
            if (_distance == 0) return 0.0;
            return _durationMin / _distance;
        }
    }
}

public class Cycling : Activity
{
    // Specific private data
    private double _speed; 

    /// <summary>
    /// Initializes a new instance of the Cycling activity.
    /// </summary>
    public Cycling(string dateStr, double durationMin, double speedKph) : base(dateStr, durationMin) // Inherits shared attributes
    {
        _speed = speedKph; // Stored specific private data
    }

    public override double DistanceKm
    {
        get
        {
            // Accessing protected member _durationMin (fixed CS0122)
            double timeHours = _durationMin / 60.0;
            return _speed * timeHours;
        }
    }

    public override double SpeedKph => _speed; // Method overriding: Returns the stored speed.

    public override double PaceMinKm 
    {
        get
        {
            // Method overriding: Pace = Time (in minutes) / Distance
            double distance = DistanceKm;
            if (distance == 0) return 0.0;
            return _durationMin / distance;
        }
    }
}

public class Swimming : Activity
{
    // Specific private data
    private int _laps; 

    /// <summary>
    /// Initializes a new instance of the Swimming activity.
    /// </summary>
    public Swimming(string dateStr, double durationMin, int laps) : base(dateStr, durationMin) // Inherits shared attributes
    {
        _laps = laps; // Stored specific private data
    }

    public override double DistanceKm
    {
        get
        {
            // Accessing protected member LapPoolLengthKm (fixed CS0880)
            // Method overriding: Distance = Laps * Length of one lap
            return _laps * LapPoolLengthKm;
        }
    }

    public override double SpeedKph 
    {
        get
        {
            // Accessing protected member _durationMin (fixed CS0122)
            double distance = DistanceKm;
            if (_durationMin == 0) return 0.0;
            double timeHours = _durationMin / 60.0;
            return distance / timeHours;
        }
    }

    public override double PaceMinKm 
    {
        get
        {
            // Accessing protected member _durationMin (fixed CS0122)
            double distance = DistanceKm;
            if (distance == 0) return 0.0;
            return _durationMin / distance;
        }
    }
}