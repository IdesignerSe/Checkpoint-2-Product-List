using System;
using System.Collections.Generic;

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
        Console.WriteLine("==== PRODUCT LIST APPLICATION ===="); // Display the application title

        Console.WriteLine("Enter the number of products:"); // Prompt the user to enter the number of products

        List<Product> products = new List<Product>(); // Create a list to store products

        int count = int.Parse(Console.ReadLine() ?? "0"); // Read the number of products from user input and convert it to an integer
        
        for (int i = 0; i < count; i++)
        {
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

            Console.WriteLine("Product added successfully! "); // Display the product list header
        }

        Console.WriteLine("==== PRODUCT LIST  ===="); // Display the product list header

            foreach (var p in products)
            {
                Console.WriteLine($"- {p.Category} | {p.Name} | {p.Price:NO} kr"); // Display each product's details in a formatted manner
            }     
    }
}