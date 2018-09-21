using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.Data.DataTransferObjects;
using ProductShop.Models;

namespace ProductShop.App
{
    public class StartUp
    {
	static void Main()
	{
	    var mapperConfiguration = new MapperConfiguration(cfg
		=> cfg.AddProfile<ProductShopProfile>());
	    IMapper mapper = mapperConfiguration.CreateMapper();
	    using (var context = new ProductShopDbContext())
	    {
		#region /* Uncomment and run to initialize database and recreate missing output files*/
		//InitializeDatabase(context, mapper);
		//GetProductsInRange(context, 1000M, 2000M);
		//GetSoldProducts(context, mapper);
		//GetCategoriesByProductCount(context);
		//GetUsersAndProducts(context);
		#endregion
	    }
	}

	private static void InitializeDatabase(ProductShopDbContext context, IMapper mapper)
	{
	    context.Database.Migrate();
	    ImportCategories(context, mapper);
	    ImportUsers(context, mapper);
	    ImportProducts(context, mapper);
	    SeedCategoriesProducts(context, mapper);
	}

	private static void ImportCategories(ProductShopDbContext context, IMapper mapper)
	{
	    string categoriesXml = File.ReadAllText(@"..\..\..\Resources\categories.xml");
	    var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
	    var categoryDtos = (CategoryDto[])serializer.Deserialize(new StringReader(categoriesXml));
	    for (int i = 0; i < categoryDtos.Length; i++)
	    {
		CategoryDto categoryDto = categoryDtos[i];
		if (IsObjectValid(categoryDto))
		{
		    Category category = mapper.Map<Category>(categoryDto);
		    if (!context.Categories.Local.Any(c => c.Name == category.Name)
			&& !context.Categories.Any(c => c.Name == category.Name))
		    {
			context.Categories.Add(category);
		    }
		}
	    }
	    context.SaveChanges();
	}

	private static void ImportUsers(ProductShopDbContext context, IMapper mapper)
	{
	    string usersXml = File.ReadAllText(@"..\..\..\Resources\users.xml");
	    var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
	    var userDtos = (UserDto[])serializer.Deserialize(new StringReader(usersXml));
	    for (int i = 0; i < userDtos.Length; i++)
	    {
		UserDto userDto = userDtos[i];
		if (IsObjectValid(userDto))
		{
		    User user = mapper.Map<User>(userDto);
		    if (!context.Users.Local.Any(u => u.FirstName == user.FirstName && u.LastName == user.LastName && u.Age == user.Age)
			&& !context.Users.Any(u => u.FirstName == user.FirstName && u.LastName == user.LastName && u.Age == user.Age))
		    {
			context.Users.Add(user);
		    }
		}
	    }
	    context.SaveChanges();
	}

	private static void ImportProducts(ProductShopDbContext context, IMapper mapper)
	{
	    List<User> users = context.Users.ToList();
	    if (users.Count == 0)
	    {
		ImportUsers(context, mapper);
		users = context.Users.ToList();
	    }
	    string productsXml = File.ReadAllText(@"..\..\..\Resources\products.xml");
	    var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
	    var productDtos = (ProductDto[])serializer.Deserialize(new StringReader(productsXml));
	    List<int> sellerIds = users.Select(u => u.Id).Take(users.Count / 2).ToList();
	    List<int> buyers = users.Select(u => u.Id).Skip(sellerIds.Count).ToList();
	    Random rng = new Random();
	    for (int i = 0; i < productDtos.Length; i++)
	    {
		ProductDto productDto = productDtos[i];
		if (IsObjectValid(productDto))
		{
		    Product product = mapper.Map<Product>(productDto);
		    int sellerId = rng.Next(sellerIds.Min(), sellerIds.Max());
		    int? buyerId = null;
		    if (i % 5 != 0) buyerId = rng.Next(buyers.Min(), buyers.Max());
		    product.SellerId = sellerId;
		    product.BuyerId = buyerId;
		    if (!context.Products.Local.Any(p => p.Name == product.Name)
			&& !context.Products.Any(p => p.Name == product.Name))
		    {
			context.Products.Add(product);
		    }
		}
	    }
	    context.SaveChanges();
	}

	private static void SeedCategoriesProducts(ProductShopDbContext context, IMapper mapper)
	{
	    List<Category> categories = context.Categories.ToList();
	    if (categories.Count == 0)
	    {
		ImportCategories(context, mapper);
		categories = context.Categories.ToList();
	    }
	    List<Product> products = context.Products.ToList();
	    if (products.Count == 0)
	    {
		ImportProducts(context, mapper);
		products = context.Products.ToList();
	    }
	    List<int> productIds = products.Select(p => p.Id).OrderBy(id => id).ToList();
	    List<int> categoryIds = categories.Select(c => c.Id).ToList();
	    Random rng = new Random();
	    foreach (int productId in productIds)
	    {
		int categoryId = rng.Next(categoryIds.Min(), categoryIds.Max());
		var categoryProduct = new CategoryProduct()
		{
		    ProductId = productId,
		    CategoryId = categoryId
		};
		if (!context.CategoryProducts.Local.Any(cp => cp.ProductId == productId)
		    && !context.CategoryProducts.Any(cp => cp.ProductId == productId))
		{
		    context.CategoryProducts.Add(categoryProduct);
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
	    ProductBuyerDto[] products = context.Products
		.Where(p => p.Buyer != null && p.Price >= minPrice && p.Price <= maxPrice)
		.OrderBy(p => p.Price)
		.Select(p => new ProductBuyerDto()
		{
		    Name = p.Name,
		    Price = p.Price,
		    Buyer = $"{(p.Buyer.FirstName != null ? $"{p.Buyer.FirstName} " : String.Empty)}{p.Buyer.LastName}"
		})
		.ToArray();
	    var serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
	    var serializer = new XmlSerializer(typeof(ProductBuyerDto[]), new XmlRootAttribute("products"));
	    using (StreamWriter writer = new StreamWriter(@"..\..\..\Output\products-in-range.xml", false))
	    {
		serializer.Serialize(writer, products, serializerNamespaces);
	    }
	}

	private static void GetSoldProducts(ProductShopDbContext context, IMapper mapper)
	{
	    UserProductsSoldDto[] usersProductsSold = context.Users
		.Where(u => u.ProductsSold.Count >= 1)
		.OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
		.Select(u => new UserProductsSoldDto()
		{
		    FirstName = u.FirstName,
		    LastName = u.LastName,
		    ProductsSold = mapper.Map<ProductDto[]>(u.ProductsSold)
		})
		.ToArray();
	    var serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
	    var serializer = new XmlSerializer(typeof(UserProductsSoldDto[]), new XmlRootAttribute("users"));
	    using (StreamWriter writer = new StreamWriter(@"..\..\..\Output\users-sold-products.xml", false))
	    {
		serializer.Serialize(writer, usersProductsSold, serializerNamespaces);
	    }
	}

	private static void GetCategoriesByProductCount(ProductShopDbContext context)
	{
	    CategoryProductsDto[] categoriesProducts = context.Categories
		.Include(c => c.CategoryProducts).ThenInclude(cp => cp.Product)
		.Where(c => c.CategoryProducts.Count > 0)
		.OrderBy(c => c.CategoryProducts.Count)
		.Select(c => new CategoryProductsDto()
		{
		    Name = c.Name,
		    ProductsCount = c.CategoryProducts.Count,
		    ProductsAveragePrice = String.Format("{0:#.00}", c.CategoryProducts.Average(cp => cp.Product.Price)),
		    TotalRevenue = String.Format("{0:#.00}", c.CategoryProducts.Sum(cp => cp.Product.Price))
		})
		.ToArray();
	    var serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
	    var serializer = new XmlSerializer(typeof(CategoryProductsDto[]), new XmlRootAttribute("categories"));
	    using (StreamWriter writer = new StreamWriter(@"..\..\..\Output\categories-by-products.xml", false))
	    {
		serializer.Serialize(writer, categoriesProducts, serializerNamespaces);
	    }
	}

	private static void GetUsersAndProducts(ProductShopDbContext context)
	{
	    var users = new UsersDto()
	    {
		UsersCount = context.Users.Where(u => u.ProductsSold.Count >= 1).Count(),
		UsersProducts = context.Users.Include(u => u.ProductsSold)
		    .Where(u => u.ProductsSold.Count >= 1)
		    .OrderByDescending(u => u.ProductsSold.Count)
		    .ThenBy(u => u.LastName)
		    .Select(u => new UserSoldProductsDto()
		    {
			FirstName = u.FirstName,
			LastName = u.LastName,
			Age = u.Age.ToString(),
			SoldProducts = new SoldProductsDto()
			{
			    ProductsCounts = u.ProductsSold.Count,
			    Products = u.ProductsSold.Select(p => new ProductPriceDto()
			    {
				Name = p.Name,
				Price = p.Price
			    }).ToArray(),
			}
		    })
		    .ToArray()
	    };
	    var serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
	    var serializer = new XmlSerializer(typeof(UsersDto), new XmlRootAttribute("users"));
	    using (StreamWriter writer = new StreamWriter(@"..\..\..\Output\users-and-products.xml", false))
	    {
		serializer.Serialize(writer, users, serializerNamespaces);
	    }
	}
    }
}
