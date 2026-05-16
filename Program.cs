using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;


public class Product
{
    private static int nextId = 1;   // auto‑increment counter
    public int Id { get; set; } = nextId++;   // unique ID

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
            $"{p.Id,-4} | {p.Category,-12} | {p.Name,-25} | {p.Price.ToString("C", CultureInfo.CurrentCulture),12}"            );
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
            $"{p.Id,-4} | {p.Category,-12} | {p.Name,-25} | {p.Price.ToString("C", CultureInfo.CurrentCulture),12}"            );
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Total Price: {CalculateTotal().ToString("C", CultureInfo.CurrentCulture)}");
        Console.ResetColor();
        Console.WriteLine();
    }

    public void EditProduct()
{
    Console.Write("Enter the Product ID to edit: ");
    string input = (Console.ReadLine() ?? "").Trim();

    if (!int.TryParse(input, out int id))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Invalid ID format.");
        Console.ResetColor();
        return;
    }

    // Find product using LINQ
    var product = products.FirstOrDefault(p => p.Id == id);

    if (product == null)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No product found with that ID.");
        Console.ResetColor();
        return;
    }

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\nEditing Product:");
    Console.ResetColor();

    Console.WriteLine(
        $"- {product.Category,-12} | {product.Name,-25} | {product.Price.ToString("C", CultureInfo.CurrentCulture),12}"
    );
    Console.WriteLine();

    // EDIT CATEGORY
    Console.Write("Enter new Category (leave empty to keep current): ");
    string newCategory = (Console.ReadLine() ?? "").Trim();
    if (!string.IsNullOrWhiteSpace(newCategory))
        product.Category = newCategory;

    // EDIT NAME
    Console.Write("Enter new Product Name (leave empty to keep current): ");
    string newName = (Console.ReadLine() ?? "").Trim();
    if (!string.IsNullOrWhiteSpace(newName))
        product.Name = newName;

    // EDIT PRICE
    Console.Write("Enter new Price (leave empty to keep current): ");
    string newPriceInput = (Console.ReadLine() ?? "").Trim();

    if (!string.IsNullOrWhiteSpace(newPriceInput))
    {
        if (decimal.TryParse(newPriceInput, out decimal newPrice) && newPrice > 0)
        {
            product.Price = newPrice;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Invalid price. Keeping old price.");
            Console.ResetColor();
        }
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Product updated successfully!");
    Console.ResetColor();
    Console.WriteLine();
}

    public void DeleteProduct()
{
    Console.Write("Enter the Product ID to delete: ");
    string input = (Console.ReadLine() ?? "").Trim();

    if (!int.TryParse(input, out int id))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Invalid ID format.");
        Console.ResetColor();
        return;
    }

    // Find product using LINQ
    var product = products.FirstOrDefault(p => p.Id == id);

    if (product == null)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No product found with that ID.");
        Console.ResetColor();
        return;
    }

    // Confirm deletion
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Are you sure you want to delete ID {product.Id}: '{product.Name}'? (y/n)");
    Console.ResetColor();

    string confirm = (Console.ReadLine() ?? "").Trim().ToLower();

    if (confirm == "y")
    {
        products.Remove(product);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Product deleted successfully!");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Deletion cancelled.");
        Console.ResetColor();
    }

    Console.WriteLine();
}

    // =====================
    // SORTING METHODS
    // =====================

    private void SortByPriceAscending()
    {
        products = products.OrderBy(p => p.Price).ToList();
    }

    private void SortByPriceDescending()
    {
        products = products.OrderByDescending(p => p.Price).ToList();
    }

    private void SortByNameAscending()
    {
        products = products.OrderBy(p => p.Name).ToList();
    }

    private void SortByNameDescending()
    {
        products = products.OrderByDescending(p => p.Name).ToList();
    }

    private void SortByCategory()
    {
        products = products.OrderBy(p => p.Category).ToList();
    }

    private void SortByIdAscending()
    {
        products = products.OrderBy(p => p.Id).ToList();
    }

    private void SortByIdDescending()
    {
        products = products.OrderByDescending(p => p.Id).ToList();
    }

    public void FilterByPriceRange()
{
    Console.WriteLine("\n=== FILTER BY PRICE RANGE ===");

    Console.Write("Enter minimum price: ");
    string minInput = (Console.ReadLine() ?? "").Trim();

    Console.Write("Enter maximum price: ");
    string maxInput = (Console.ReadLine() ?? "").Trim();

    if (!decimal.TryParse(minInput, out decimal minPrice) ||
        !decimal.TryParse(maxInput, out decimal maxPrice))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Invalid price format.");
        Console.ResetColor();
        return;
    }

    if (minPrice > maxPrice)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Minimum price cannot be greater than maximum price.");
        Console.ResetColor();
        return;
    }

    // LINQ filtering
    var filtered = products
        .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
        .ToList();

    if (!filtered.Any())
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No products found in this price range.");
        Console.ResetColor();
        return;
    }

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"\nProducts between {minPrice:C} and {maxPrice:C}:");
    Console.ResetColor();

    foreach (var p in filtered)
    {
        Console.WriteLine(
            $"{p.Id,-4} | {p.Category,-12} | {p.Name,-25} | {p.Price.ToString("C", CultureInfo.CurrentCulture),12}"
        );
    }

    Console.WriteLine();
}


    public void ShowStatistics()
{
    if (!products.Any())
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No products available to calculate statistics.");
        Console.ResetColor();
        return;
    }

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("==== STATISTICS DASHBOARD ====");
    Console.ResetColor();
    Console.WriteLine();

    // Most expensive product
    var mostExpensive = products
        .OrderByDescending(p => p.Price)
        .First();

    // Cheapest product
    var cheapest = products
        .OrderBy(p => p.Price)
        .First();

    // Average price
    decimal averagePrice = products.Average(p => p.Price);

    // Count per category
    var categoryCounts = products
        .GroupBy(p => p.Category)
        .Select(g => new { Category = g.Key, Count = g.Count() })
        .ToList();

    // Display results
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Most Expensive: {mostExpensive.Name} ({mostExpensive.Price.ToString("C", CultureInfo.CurrentCulture)})");
    Console.WriteLine($"Cheapest:       {cheapest.Name} ({cheapest.Price.ToString("C", CultureInfo.CurrentCulture)})");
    Console.WriteLine($"Average Price:  {averagePrice.ToString("C", CultureInfo.CurrentCulture)}");
    Console.ResetColor();

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Product Count per Category:");
    Console.ResetColor();

    foreach (var c in categoryCounts)
    {
        Console.WriteLine($"- {c.Category}: {c.Count}");
    }

    Console.WriteLine();
}

    public void SaveProducts()
{
    try
    {
        string json = JsonSerializer.Serialize(products, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText("products.json", json);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Products saved successfully to products.json");
        Console.ResetColor();
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error saving products: " + ex.Message);
        Console.ResetColor();
    }
}

    public void LoadProducts()
{
    try
    {
        if (!File.Exists("products.json"))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("No saved file found.");
            Console.ResetColor();
            return;
        }

        string json = File.ReadAllText("products.json");

        var loadedProducts = JsonSerializer.Deserialize<List<Product>>(json);

        if (loadedProducts != null)
        {
            products = loadedProducts;

            // IMPORTANT: Fix ID counter so new products get correct IDs
            if (products.Any())
            {
                int maxId = products.Max(p => p.Id);
                typeof(Product)
                    .GetField("nextId", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                    ?.SetValue(null, maxId + 1);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Products loaded successfully!");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error loading products: " + ex.Message);
        Console.ResetColor();
    }
}

    public void SortMenu()
{

    
    Console.WriteLine("\n=== SORT PRODUCTS ===");
    Console.WriteLine("1. Price (Low → High)");
    Console.WriteLine("2. Price (High → Low)");
    Console.WriteLine("3. Name (A → Z)");
    Console.WriteLine("4. Name (Z → A)");
    Console.WriteLine("5. Category (A → Z)");
    Console.WriteLine("6. ID (Ascending)");
    Console.WriteLine("7. ID (Descending)");
    Console.Write("Choose an option: ");

    

    string choice = (Console.ReadLine() ?? "").Trim();

    switch (choice)
    {
        case "1":
            SortByPriceAscending();
            break;

        case "2":
            SortByPriceDescending();
            break;

        case "3":
            SortByNameAscending();
            break;

        case "4":
            SortByNameDescending();
            break;

        case "5":
            SortByCategory();
            break;

        case "6":
            SortByIdAscending();
            break;

        case "7":
            SortByIdDescending();
            break;

        default:
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Invalid choice.");
            Console.ResetColor();
            return;
        
    }
    

    Console.WriteLine();
    ShowProducts(); // show sorted list immediately
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
            Console.WriteLine("4. Edit Product");
            Console.WriteLine("5. Delete Product");
            Console.WriteLine("6. Show Statistics");
            Console.WriteLine("7. Save Products");
            Console.WriteLine("8. Load Products");
            Console.WriteLine("9. Exit");
            Console.WriteLine("10. Sort Products");
            Console.WriteLine("11. Filter by Price Range");
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
                    manager.EditProduct();
                    break;

                case "5":
                    manager.DeleteProduct();
                    break;
                
                case "6":
                   manager.ShowStatistics();
                    break;

                case "7":
                    manager.SaveProducts();
                    break;

                case "8":
                    manager.LoadProducts();
                    break;

                case "9":
                    running = false;
                    break;

                case "10":
                    manager.SortMenu(); 
                    break;

                case "11":
                    manager.FilterByPriceRange();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Invalid choice. Please try again. Please enter a number between 1 and 9.");
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
