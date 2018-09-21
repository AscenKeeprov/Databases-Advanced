using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop.App
{
    public class StartUp
    {
	static void Main()
	{
	    using (var context = new ProductShopDbContext())
	    {
		#region /* Uncomment and run to initialize database and recreate missing output files*/
		//InitializeDatabase(context);
		//GetProductsInRange(context, 500M, 1000M);
		//GetSoldProducts(context);
		//GetCategoriesByProductCount(context);
		//GetUsersAndProducts(context);
		#endregion
	    }
	}

	private static void InitializeDatabase(ProductShopDbContext context)
	{
	    context.Database.Migrate();
	    ImportCategories(context);
	    ImportUsers(context);
	    ImportProducts(context);
	    SeedCategoriesProducts(context);
	}

	private static void ImportCategories(ProductShopDbContext context)
	{
	    using (StreamReader categoriesJSON = File.OpenText(@"..\..\..\Resources\categories.json"))
	    {
		Category[] categories = JsonConvert.DeserializeObject<Category[]>(categoriesJSON.ReadToEnd());
		HashSet<Category> existingCategories = context.Categories.ToHashSet();
		foreach (Category category in categories)
		{
		    if (IsObjectValid(category) && !existingCategories.Any(c => c.Name == category.Name))
		    {
			context.Categories.Add(category);
			existingCategories.Add(category);
		    }
		}
		context.SaveChanges();
	    }
	}

	private static void ImportUsers(ProductShopDbContext context)
	{
	    using (StreamReader usersJSON = File.OpenText(@"..\..\..\Resources\users.json"))
	    {
		User[] users = JsonConvert.DeserializeObject<User[]>(usersJSON.ReadToEnd());
		HashSet<User> existingUsers = context.Users.ToHashSet();
		foreach (User user in users)
		{
		    if (IsObjectValid(user) && !existingUsers.Any(u => u.LastName == user.LastName
			&& u.FirstName == user.FirstName && u.Age == user.Age))
		    {
			context.Users.Add(user);
			existingUsers.Add(user);
		    }
		}
		context.SaveChanges();
	    }
	}

	private static void ImportProducts(ProductShopDbContext context)
	{
	    if (!context.Users.Any()) ImportUsers(context);
	    HashSet<User> users = context.Users.ToHashSet();
	    using (StreamReader productsJSON = File.OpenText(@"..\..\..\Resources\products.json"))
	    {
		Product[] products = JsonConvert.DeserializeObject<Product[]>(productsJSON.ReadToEnd());
		List<int> sellerIds = users.Select(u => u.Id).Take(users.Count / 2).ToList();
		List<int> buyerIds = users.Select(u => u.Id).Skip(sellerIds.Count).ToList();
		Random rng = new Random();
		HashSet<Product> existingProducts = context.Products.ToHashSet();
		for (int i = 0; i < products.Length; i++)
		{
		    Product product = products[i];
		    if (IsObjectValid(product) && !existingProducts.Any(p => p.Name == product.Name))
		    {
			product.SellerId = sellerIds[rng.Next(0, sellerIds.Count)];
			if (i % 5 != 0) product.BuyerId = buyerIds[rng.Next(0, buyerIds.Count)];
			context.Products.Add(product);
			existingProducts.Add(product);
		    }
		}
		context.SaveChanges();
	    }
	}

	private static void SeedCategoriesProducts(ProductShopDbContext context)
	{
	    if (!context.Categories.Any()) ImportCategories(context);
	    List<int> categoryIds = context.Categories.Select(c => c.Id).ToList();
	    if (!context.Products.Any()) ImportProducts(context);
	    List<int> productIds = context.Products.Select(p => p.Id).OrderBy(id => id).ToList();
	    Random rng = new Random();
	    HashSet<CategoryProduct> categoryProducts = context.CategoryProducts.ToHashSet();
	    foreach (int productId in productIds)
	    {
		CategoryProduct categoryProduct = new CategoryProduct() { ProductId = productId };
		if (!categoryProducts.Any(cp => cp.ProductId == productId))
		{
		    categoryProduct.CategoryId = categoryIds[rng.Next(0, categoryIds.Count - 1)];
		    context.CategoryProducts.Add(categoryProduct);
		    categoryProducts.Add(categoryProduct);
		}
	    }
	    context.SaveChanges();
	}

	private static bool IsObjectValid(object obj)
	{
	    var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
	    var validationResults = new List<ValidationResult>();
	    bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
	    return isValid;
	}

	private static void GetProductsInRange(ProductShopDbContext context, decimal minPrice, decimal maxPrice)
	{
	    var productsSellers = context.Products
		.Where(p => p.Price >= minPrice && p.Price <= maxPrice)
		.OrderBy(p => p.Price)
		.Select(p => new
		{
		    name = p.Name,
		    price = p.Price,
		    seller = $"{(p.Seller.FirstName != null ? $"{p.Seller.FirstName} " : String.Empty)}{p.Seller.LastName}"
		}).ToArray();
	    string output = JsonConvert.SerializeObject(productsSellers, Formatting.Indented);
	    File.WriteAllText(@"..\..\..\Output\products-in-range.json", output, Encoding.UTF8);
	}

	private static void GetSoldProducts(ProductShopDbContext context)
	{
	    var usersProductsSold = context.Users
		.Where(u => u.ProductsSold.Count >= 1 && u.ProductsSold.Any(p => p.Buyer != null))
		.OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
		.Select(u => new
		{
		    firstName = u.FirstName,
		    lastName = u.LastName,
		    soldProducts = u.ProductsSold.Where(p => p.Buyer != null).Select(p => new
		    {
			name = p.Name,
			price = p.Price,
			buyerFirstName = p.Buyer.FirstName,
			buyerLastName = p.Buyer.LastName
		    }).ToArray()
		}).ToArray();
	    string output = JsonConvert.SerializeObject(usersProductsSold, Formatting.Indented);
	    File.WriteAllText(@"..\..\..\Output\users-sold-products.json", output, Encoding.UTF8);
	}

	private static void GetCategoriesByProductCount(ProductShopDbContext context)
	{
	    var categoriesProductStats = context.Categories
		.OrderBy(c => c.Name)
		.Select(c => new
		{
		    category = c.Name,
		    productsCount = c.CategoryProducts.Count,
		    averagePrice = c.CategoryProducts.Any() ? c.CategoryProducts.Sum(cp => cp.Product.Price) / c.CategoryProducts.Count : 0,
		    totalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
		}).ToArray();
	    string output = JsonConvert.SerializeObject(categoriesProductStats, Formatting.Indented);
	    File.WriteAllText(@"..\..\..\Output\categories-by-products.json", output, Encoding.UTF8);
	}

	private static void GetUsersAndProducts(ProductShopDbContext context)
	{
	    var usersProducts = new
	    {
		usersCount = context.Users.Where(u => u.ProductsSold.Count >= 1).Count(),
		users = context.Users
		    .Where(u => u.ProductsSold.Count >= 1)
		    .OrderByDescending(u => u.ProductsSold.Count)
		    .ThenBy(u => u.LastName)
		    .Select(u => new
		    {
			firstName = u.FirstName,
			lastName = u.LastName,
			age = u.Age,
			soldProducts = new
			{
			    count = u.ProductsSold.Count,
			    products = u.ProductsSold.Select(p => new
			    {
				name = p.Name,
				price = p.Price
			    }).ToArray(),
			}
		    })
		    .ToArray()
	    };
	    string output = JsonConvert.SerializeObject(usersProducts, new JsonSerializerSettings()
	    {
		Formatting = Formatting.Indented,
		NullValueHandling = NullValueHandling.Ignore
	    });
	    File.WriteAllText(@"..\..\..\Output\users-and-products.json", output, Encoding.UTF8);
	}
    }
}
