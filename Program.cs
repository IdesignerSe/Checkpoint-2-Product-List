using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class Product
{
    public string Category { get; set; } = "";
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}

public class ProductManager
{
    private List<Product> products = new List<Product>();

    public void AddProduct()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Enter product details:");

        // CATEGORY VALIDATION
        string category;
        do
        {
            Console.Write("Enter Category: ");
            category = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(category))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Category cannot be empty. Please try again.");
                Console.ResetColor();
            }

        } while (string.IsNullOrWhiteSpace(category));

        
        // NAME VALIDATION
        string name;
        do
        {
            Console.Write("Enter Product Name: ");
            name = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Product name cannot be empty. Please try again.");
                Console.ResetColor();
            }

        } while (string.IsNullOrWhiteSpace(name));

        // PRICE VALIDATION
        decimal price;
        while (true)
        {
            Console.Write("Enter Price: ");
            string input = (Console.ReadLine() ?? "").Trim();

            if (decimal.TryParse(input, out price) && price > 0)
            {
                break;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Invalid price. Please enter a positive number.");
            Console.ResetColor();
        }
        products.Add(new Product
        {
            Category = category,
            Name = name,
            Price = price
        });

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.WriteLine("Product added successfully!");
        Console.ResetColor();
        Console.WriteLine();
    }

    public void ShowProducts()
    {
        // Sort by price (lowest → highest)
        var sorted = products
            .OrderBy(p => p.Price)
            .ThenBy(p => p.Category)
            .ThenBy(p => p.Name)
            .ToList();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==== PRODUCT LIST ====");
        Console.ResetColor();
        Console.WriteLine();

        foreach (var p in sorted)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
                $"- {p.Category,-12} | {p.Name,-25} | {p.Price.ToString("C", CultureInfo.CurrentCulture),12}"
            );
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Total Price: {CalculateTotal().ToString("C", CultureInfo.CurrentCulture)}");
        Console.ResetColor();
        Console.WriteLine();
    }

    public decimal CalculateTotal()
    {
        return products.Sum(p => p.Price);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine();
        Console.WriteLine("==== PRODUCT LIST APPLICATION ====");
        Console.WriteLine();

        ProductManager manager = new ProductManager();

        bool running = true;

        while (running)
        {
            manager.AddProduct();

            Console.Write("Do you want to add another product? (y/n): ");
            string answer = (Console.ReadLine() ?? "").Trim().ToLower();

            if (answer == "n")
            {
                Console.WriteLine();
                manager.ShowProducts();
                running = false;
            }
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("All products have been added and calculated successfully. Thank you!");
        Console.ResetColor();
        Console.WriteLine();
    }
}
