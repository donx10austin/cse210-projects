
// This is the 'Product' class, translated from your Python code.
// It represents a single product with its properties.
public class Product
{
    // Properties are read-only after initialization.
    public string ProductId { get; }
    public string Name { get; }
    public double Price { get; }
    public int Quantity { get; }
    public double Weight { get; }

    /// <summary>
    /// The constructor initializes a new Product object.
    /// </summary>
    public Product(string productId, string name, double price, int quantity, double weight)
    {
        ProductId = productId;
        Name = name;
        Price = price;
        Quantity = quantity;
        Weight = weight;
    }

    /// <summary>
    /// Returns the total cost of this product (price * quantity).
    /// </summary>
    public double TotalCost()
    {
        return Price * Quantity;
    }

    /// <summary>
    /// Provides a string representation of the object.
    /// </summary>
    public override string ToString()
    {
        return $"Product(id='{ProductId}', name='{Name}', price=${Price:F2}, quantity={Quantity}, weight={Weight} lbs)";
    }
}

// This is the 'Program' class. It contains the Main method, which is the application's entry point.
public class Program
{
    public static void Main(string[] args)
    {
        // Example usage of your Product class.
        // You can replace this with your actual program logic.

        // Create a new Product object.
        Product laptop = new Product("A001", "Laptop", 1200.50, 2, 4.5);

        // Display the product details to the console.
        Console.WriteLine(laptop.ToString());

        // Calculate and display the total cost.
        double total = laptop.TotalCost();
        Console.WriteLine($"Total cost for {laptop.Quantity} {laptop.Name}s is: ${total:F2}");
    }
}