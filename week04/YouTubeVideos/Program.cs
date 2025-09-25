using System;
using System.Collections.Generic;
using System.Linq;

// The 'Video' class is designed to store all relevant information for a single YouTube video.
// It acts as a blueprint for creating objects that hold video data.
public class Video
{
    // These properties act as "containers" for the video's information.
    public string VideoId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthSeconds { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    // We also include a list to store all comments related to this video.
    public List<Comment> Comments { get; set; }

    /// <summary>
    /// The constructor initializes a new Video object with its properties.
    /// </summary>
    public Video(string videoId, string title, string author, int lengthSeconds, string description, string url)
    {
        VideoId = videoId;
        Title = title;
        Author = author;
        LengthSeconds = lengthSeconds;
        Description = description;
        Url = url;
        Comments = new List<Comment>(); // Initialize the list of comments
    }

    /// <summary>
    /// Returns the total number of comments for this video.
    /// </summary>
    public int GetCommentCount()
    {
        return Comments.Count;
    }

    // This method provides a clear string representation of the object,
    // which is useful for debugging and printing. (Similar to Python's __repr__)
    public override string ToString()
    {
        return $"Video(id='{VideoId}', title='{Title}', author='{Author}', length={LengthSeconds}s, comments={GetCommentCount()})";
    }
}

// The 'Comment' class is a simple data structure for a single comment.
// It holds information about the comment's author, text, and creation date.
public class Comment
{
    public string CommentId { get; set; }
    public string Author { get; set; }
    public string Text { get; set; }
    public string Timestamp { get; set; } // Using string for simplicity as in Python example

    /// <summary>
    /// The constructor initializes the Comment object with its properties.
    /// </summary>
    public Comment(string commentId, string author, string text, string timestamp)
    {
        CommentId = commentId;
        Author = author;
        Text = text;
        Timestamp = timestamp;
    }

    // Provides a clear string representation of the Comment object. (Similar to Python's __repr__)
    public override string ToString()
    {
        return $"Comment(id='{CommentId}', author='{Author}', text='{Text}')";
    }
}

// --- Main Program Logic ---
public class Program
{
    public static void Main(string[] args)
    {
        // We will use one list to act as our "database" for storing all the videos.
        List<Video> allVideos = new List<Video>();

        // 1. Create the first video and its comments.
        Video video1 = new Video("v123", "Unboxing the 'Cosmic Sound' Headphones", "TechReviewer", 620, "I'm excited to unbox the new headphones!", "http://youtube.com/v123");
        video1.Comments.Add(new Comment("c101", "Alice", "Wow, I love the Cosmic Sound headphones!", "2024-05-20"));
        video1.Comments.Add(new Comment("c102", "Bob", "Does this brand make other headsets too?", "2024-05-21"));
        video1.Comments.Add(new Comment("c103", "Charlie", "The sound quality on these is amazing.", "2024-05-21"));
        video1.Comments.Add(new Comment("c104", "Diana", "Can you review the 'Quantum Bass' headset next?", "2024-05-22"));
        allVideos.Add(video1);
        
        // 2. Create the second video and its comments.
        Video video2 = new Video("v456", "My Top 5 Gaming Headsets", "GamerPro", 450, "A review of the best headsets for gaming.", "http://youtube.com/v456");
        video2.Comments.Add(new Comment("c201", "Evan", "I bought one based on your video, thanks!", "2024-05-22"));
        video2.Comments.Add(new Comment("c202", "Fiona", "Great breakdown of the pros and cons.", "2024-05-23"));
        video2.Comments.Add(new Comment("c203", "George", "Any thoughts on the 'Sonic' brand?", "2024-05-23"));
        video2.Comments.Add(new Comment("c204", "Hannah", "Very helpful, subbed!", "2024-05-24"));
        allVideos.Add(video2);

        // 3. Create the third video and its comments.
        Video video3 = new Video("v789", "Cooking with a Smart Oven", "ChefJulia", 380, "Today we're trying out the new 'SmartChef' oven.", "http://youtube.com/v789");
        video3.Comments.Add(new Comment("c301", "Ian", "This oven looks so futuristic!", "2024-05-25"));
        video3.Comments.Add(new Comment("c302", "Jasmine", "Does it preheat faster than a regular oven?", "2024-05-25"));
        video3.Comments.Add(new Comment("c303", "Kyle", "I need one of these in my kitchen.", "2024-05-26"));
        video3.Comments.Add(new Comment("c304", "Laura", "Love your channel, Julia!", "2024-05-26"));
        allVideos.Add(video3);

        // 4. Create the fourth video and its comments.
        Video video4 = new Video("v901", "Running 10K in 40 Minutes", "FitnessFreak", 550, "My tips and tricks to improve your 10K time.", "http://youtube.com/v901");
        video4.Comments.Add(new Comment("c401", "Mark", "Your advice on pacing was a game-changer for me.", "2024-05-27"));
        video4.Comments.Add(new Comment("c402", "Nancy", "What shoes are you wearing?", "2024-05-27"));
        video4.Comments.Add(new Comment("c403", "Oliver", "I'm going to try this on my next run. Thanks!", "2024-05-28"));
        video4.Comments.Add(new Comment("c404", "Pam", "Great video, very inspiring.", "2024-05-28"));
        allVideos.Add(video4);

        // 5. Iterate through the list of videos and display the details for each one.
        Console.WriteLine("--- Stored Videos and Comments ---");
        for (int i = 0; i < allVideos.Count; i++)
        {
            Video video = allVideos[i];
            Console.WriteLine($"--- Video #{i + 1} ---");
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthSeconds} seconds");
            Console.WriteLine($"Number of comments: {video.GetCommentCount()}");
            
            // Display each comment associated with the current video.
            if (video.Comments.Any()) // Using Any() to check if the list has comments
            {
                Console.WriteLine("Comments:");
                foreach (Comment comment in video.Comments)
                {
                    Console.WriteLine($"    - '{comment.Text}' by {comment.Author}");
                }
            }
            Console.WriteLine(new string('-', 25)); // C# way to repeat a character
        }
    }
}