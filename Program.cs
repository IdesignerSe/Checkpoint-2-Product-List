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
            Console.WriteLine("Invalid price. Please enter a  numberic value.");
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

public void SearchProduct()
    {
        Console.Write("Enter search term (name or category): ");
        string term = (Console.ReadLine() ?? "").Trim().ToLower();

        var results = products
            .Where(p => p.Name.ToLower().Contains(term) ||
                        p.Category.ToLower().Contains(term))
            .ToList();

        if (!results.Any())
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("No matching products found.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n==== SEARCH RESULTS ====");
        Console.ResetColor();

        foreach (var p in results)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; // highlight
            Console.WriteLine(
                $"- {p.Category,-12} | {p.Name,-25} | {p.Price.ToString("C", CultureInfo.CurrentCulture),12}"
            );
            Console.ResetColor();
        }

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
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Show Products");
            Console.WriteLine("3. Search Product");
            Console.WriteLine("4. Exit");
            Console.Write("Your choice: ");

            string choice = (Console.ReadLine() ?? "").Trim();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    manager.AddProduct();
                    break;

                case "2":
                    manager.ShowProducts();
                    break;

                case "3":
                    manager.SearchProduct();
                    break;

                case "4":
                    running = false;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Invalid choice. Please try again. Please enter a number between 1 and 4.");
                    Console.ResetColor();
                    break;
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Goodbye! Thank you for using the Product List Application.");
        Console.ResetColor();
        Console.WriteLine();
    }
}
