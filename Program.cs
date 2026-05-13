using System;
using System.Collections.Generic;
using System.Globalization;


public class Product
{
    public string Category { get; set; } = "";
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}

class Program
{
    static void Main()
    {
        Console.WriteLine();
        Console.WriteLine("==== PRODUCT LIST APPLICATION ===="); // Display the application title
        Console.WriteLine(); 
        Console.WriteLine("Enter the number of products:"); // Prompt the user to enter the number of products
        Console.WriteLine(); 

        List<Product> products = new List<Product>(); // Create a list to store products

        int count = int.Parse(Console.ReadLine() ?? "0"); // Read the number of products from user input and convert it to an integer
        
        for (int i = 0; i < count; i++)
        {
            {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Enter details for product {i + 1}:"); // Prompt the user to enter details for each product
           
            Console.Write("Enter Category: ");
            string category = Console.ReadLine() ?? ""; // Read the category of the product from user input
           
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine() ?? "";
           
            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine() ?? "0");

           
            products.Add(new Product 
            { Category = category,
                         Name = name, 
                         Price = price 
            });
            Console.WriteLine(); 
            Console.WriteLine("Product added successfully! "); // Display the product list header
            Console.WriteLine(); 

        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==== PRODUCT LIST  ===="); // Display the product list header
        Console.WriteLine(); 
        Console.ResetColor();

            foreach (var p in products)
            {
                Console.WriteLine($"- {p.Category, -12} | {p.Name,-25} | {p.Price.ToString("C", CultureInfo.CurrentCulture), 12}"); // Display each product's details for globalization.
            }     

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("All products have been added successfully. Thank you!");
            Console.ResetColor();
            Console.WriteLine(); 
        }
    }
}