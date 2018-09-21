namespace P03_SalesDatabase.Data.EntityInitialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using P03_SalesDatabase.Data.Models;

    public class ProductInit
    {
	private static Random rng = new Random();

	private static string[] productManufacturers = new string[]
	{
	    "AMD",
	    "ASUS",
	    "Cooler Master",
	    "Gigabyte",
	    "Intel",
	    "Logitech",
	    "MSI",
	    "NVidia",
	    "Samsung"
	};

	private static string[] productCategories = new string[]
	{
	    "Business",
	    "Gaming",
	    "Science"
	};

	private static string[] productTypes = new string[]
	{
	    "Chassis",
	    "Graphics Card",
	    "Monitor",
	    "Motherboard",
	    "Mouse",
	    "Keyboard",
	    "Processor",
	    "Sound Card"
	};

	public static Product[] SeedProducts()
	{
	    int productsCount = rng.Next(5, 25);
	    var products = new List<Product>();
	    for (int id = 0; id < productsCount; id++)
	    {
		string manufacturer = productManufacturers[rng.Next(productManufacturers.Length)];
		string category = productCategories[rng.Next(productCategories.Length)];
		string type = productTypes[rng.Next(productTypes.Length)];
		decimal quantity = rng.Next(1, 11);
		decimal price = rng.Next(100, 700) +
		    (decimal.Parse(String.Format("{0:0.##}", new Random().NextDouble())));
		Product product = new Product()
		{
		    ProductId = products.Count + 1,
		    Name = $"{manufacturer} {category} {type}",
		    Quantity = quantity,
		    Price = price
		};
		if (!products.Any(p => p.CompareTo(product) == 0))
		    products.Add(product);
	    }
	    return products.ToArray();
	}
    }
}
