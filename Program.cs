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

        Console.Write("Enter Category: ");
        string category = (Console.ReadLine() ?? "").Trim();

        Console.Write("Enter Product Name: ");
        string name = (Console.ReadLine() ?? "").Trim();

        Console.Write("Enter Price: ");
        decimal price = decimal.Parse(Console.ReadLine() ?? "0");

        products.Add(new Product
        {
            Category = category,
            Name = name,
            Price = price
        });

        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Product added successfully!");
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

        Console.Write("Enter the number of products: ");
        int count = int.Parse(Console.ReadLine() ?? "0");
        Console.WriteLine();

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Product {i + 1}:");
            manager.AddProduct();
        }

        manager.ShowProducts();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("All products have been added successfully. Thank you!");
        Console.ResetColor();
        Console.WriteLine();
    }
}
