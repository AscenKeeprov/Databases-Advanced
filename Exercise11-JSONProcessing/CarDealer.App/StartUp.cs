using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using CarDealer.Data;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CarDealer.App
{
    public class StartUp
    {
	static void Main()
	{
	    using (var context = new CarDealerDbContext())
	    {
		#region /* Uncomment and run to initialize database and recreate missing output files*/
		//InitializeDatabase(context);
		//GetOrderedCustomers(context);
		//GetToyotaCars(context);
		//GetLocalSuppliers(context);
		//GetCarsParts(context);
		//GetTotalSalesByCustomer(context);
		//GetSalesWithDiscounts(context);
		#endregion
	    }
	}

	private static void InitializeDatabase(CarDealerDbContext context)
	{
	    context.Database.Migrate();
	    ImportSuppliers(context);
	    ImportCustomers(context);
	    ImportParts(context);
	    ImportCars(context);
	    SeedSales(context);
	}

	private static void ImportSuppliers(CarDealerDbContext context)
	{
	    using (StreamReader suppliersJSON = File.OpenText(@"..\..\..\Resources\suppliers.json"))
	    {
		Supplier[] suppliers = JsonConvert.DeserializeObject<Supplier[]>(suppliersJSON.ReadToEnd());
		HashSet<Supplier> existingSuppliers = context.Suppliers.ToHashSet();
		foreach (Supplier supplier in suppliers)
		{
		    if (IsObjectValid(supplier) && !existingSuppliers.Any(s => s.Name == supplier.Name))
		    {
			context.Suppliers.Add(supplier);
			existingSuppliers.Add(supplier);
		    }
		}
		context.SaveChanges();
	    }
	}

	private static void ImportCustomers(CarDealerDbContext context)
	{
	    using (StreamReader customersJSON = File.OpenText(@"..\..\..\Resources\customers.json"))
	    {
		Customer[] customers = JsonConvert.DeserializeObject<Customer[]>(customersJSON.ReadToEnd());
		HashSet<Customer> existingCustomers = context.Customers.ToHashSet();
		foreach (Customer customer in customers)
		{
		    if (IsObjectValid(customer) && !existingCustomers.Any(c
			=> c.Name == customer.Name && c.BirthDate == customer.BirthDate))
		    {
			context.Customers.Add(customer);
			existingCustomers.Add(customer);
		    }
		}
		context.SaveChanges();
	    }
	}

	private static void ImportParts(CarDealerDbContext context)
	{
	    if (!context.Suppliers.Any()) ImportSuppliers(context);
	    using (StreamReader partsJSON = File.OpenText(@"..\..\..\Resources\parts.json"))
	    {
		Part[] parts = JsonConvert.DeserializeObject<Part[]>(partsJSON.ReadToEnd());
		HashSet<Part> existingParts = context.Parts.ToHashSet();
		List<int> supplierIds = context.Suppliers.Select(s => s.Id).ToList();
		Random rng = new Random();
		foreach (Part part in parts)
		{
		    if (IsObjectValid(part) && !existingParts.Any(p => p.Name == part.Name))
		    {
			part.Supplier_Id = supplierIds[rng.Next(0, supplierIds.Count)];
			context.Parts.Add(part);
			existingParts.Add(part);
		    }
		}
		context.SaveChanges();
	    }
	}

	private static void ImportCars(CarDealerDbContext context)
	{
	    if (!context.Parts.Any()) ImportParts(context);
	    using (StreamReader carsJSON = File.OpenText(@"..\..\..\Resources\cars.json"))
	    {
		Car[] cars = JsonConvert.DeserializeObject<Car[]>(carsJSON.ReadToEnd());
		HashSet<Car> existingCars = context.Cars.ToHashSet();
		List<int> partIds = context.Parts.Select(p => p.Id).ToList();
		Random rng = new Random();
		foreach (Car car in cars)
		{
		    if (IsObjectValid(car) && !existingCars.Any(c
			=> c.Make == car.Make && c.Model == car.Model
			&& c.TravelledDistance == car.TravelledDistance))
		    {
			int carPartsCount = rng.Next(10, 21);
			for (int i = 1; i <= carPartsCount; i++)
			{
			    int partId = partIds[rng.Next(0, partIds.Count)];
			    while (car.CarParts.Any(p => p.Part_Id == partId))
			    {
				partId = partIds[rng.Next(0, partIds.Count)];
			    }
			    PartCar carPart = new PartCar()
			    {
				Part_Id = partId,
				Car_Id = car.Id
			    };
			    car.CarParts.Add(carPart);
			}
			context.Cars.Add(car);
			existingCars.Add(car);
		    }
		}
		context.SaveChanges();
	    }
	}

	private static void SeedSales(CarDealerDbContext context)
	{
	    if (!context.Customers.Any()) ImportCustomers(context);
	    if (!context.Cars.Any()) ImportCars(context);
	    var customers = context.Customers.Select(c => new { c.Id, c.IsYoungDriver }).ToHashSet();
	    List<int> customerIds = customers.Select(c => c.Id).ToList();
	    List<int> carIds = context.Cars.Select(c => c.Id).ToList();
	    List<decimal> discounts = new List<decimal>() { 0M, 0.05M, 0.1M, 0.15M, 0.2M, 0.3M, 0.4M, 0.5M };
	    HashSet<Sale> sales = context.Sales.ToHashSet();
	    int carsForSaleCount = carIds.Count * 100 / 110;
	    Random rng = new Random();
	    while (sales.Count < carsForSaleCount)
	    {
		int carId = carIds[rng.Next(0, carIds.Count)];
		int customerId = customerIds[rng.Next(0, customerIds.Count)];
		var customer = customers.First(c => c.Id == customerId);
		decimal discount = discounts[rng.Next(0, discounts.Count)];
		if (customer.IsYoungDriver) discount += 0.05M;
		Sale sale = new Sale()
		{
		    Car_Id = carId,
		    Customer_Id = customerId,
		    Discount = discount
		};
		context.Sales.Add(sale);
		sales.Add(sale);
		carIds.Remove(carId);
	    }
	    context.SaveChanges();
	}

	private static bool IsObjectValid(object obj)
	{
	    var validationContext = new ValidationContext(obj);
	    var validationResults = new List<ValidationResult>();
	    bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
	    return isValid;
	}

	private static void GetOrderedCustomers(CarDealerDbContext context)
	{
	    Customer[] customers = context.Customers
		.OrderBy(c => c.BirthDate)
		.ThenBy(c => c.IsYoungDriver)
		.ToArray();
	    string output = JsonConvert.SerializeObject(customers, new JsonSerializerSettings()
	    {
		Formatting = Formatting.Indented,
		NullValueHandling = NullValueHandling.Ignore
	    });
	    File.WriteAllText(@"..\..\..\Output\ordered-customers.json", output, Encoding.UTF8);
	}

	private static void GetToyotaCars(CarDealerDbContext context)
	{
	    var carsMakeToyota = context.Cars
		.Where(c => c.Make.Equals("Toyota"))
		.OrderBy(c => c.Model).ThenByDescending(c => c.TravelledDistance)
		.Select(c => new
		{
		    c.Id,
		    c.Make,
		    c.Model,
		    c.TravelledDistance
		}).ToArray();
	    string output = JsonConvert.SerializeObject(carsMakeToyota, Formatting.Indented);
	    File.WriteAllText(@"..\..\..\Output\toyota-cars.json", output, Encoding.UTF8);
	}

	private static void GetLocalSuppliers(CarDealerDbContext context)
	{
	    var suppliersLocal = context.Suppliers
		.Where(s => s.IsImporter == false)
		.Select(s => new
		{
		    s.Id,
		    s.Name,
		    PartsCount = s.PartsSupplied.Count
		}).ToArray();
	    string output = JsonConvert.SerializeObject(suppliersLocal, Formatting.Indented);
	    File.WriteAllText(@"..\..\..\Output\local-suppliers.json", output, Encoding.UTF8);
	}

	private static void GetCarsParts(CarDealerDbContext context)
	{
	    var carsParts = context.Cars.Select(c => new
	    {
		car = new
		{
		    c.Make,
		    c.Model,
		    c.TravelledDistance
		},
		parts = c.CarParts.Select(cp => new
		{
		    cp.Part.Name,
		    cp.Part.Price
		}).ToArray()
	    }).ToArray();
	    string output = JsonConvert.SerializeObject(carsParts, Formatting.Indented);
	    File.WriteAllText(@"..\..\..\Output\cars-and-parts.json", output, Encoding.UTF8);
	}

	private static void GetTotalSalesByCustomer(CarDealerDbContext context)
	{
	    var customersExpenditures = context.Customers
		.Where(c => c.Purchases.Count >= 1)
		.Select(c => new
		{
		    fullName = c.Name,
		    boughtCars = c.Purchases.Count,
		    purchases = c.Purchases.Select(s => new
		    {
			s.Discount,
			carPrice = s.Car.CarParts.Sum(cp => cp.Part.Price)
		    })
		}).Select(ce => new
		{
		    ce.fullName,
		    ce.boughtCars,
		    spentMoney = decimal.Parse(String.Format("{0:#.00}", ce.purchases
					.Sum(p => p.carPrice - p.carPrice * p.Discount)))
		})
		.OrderByDescending(ce => ce.spentMoney)
		.ThenByDescending(ce => ce.boughtCars)
		.ToArray();
	    string output = JsonConvert.SerializeObject(customersExpenditures, Formatting.Indented);
	    File.WriteAllText(@"..\..\..\Output\customers-total-sales.json", output, Encoding.UTF8);
	}

	private static void GetSalesWithDiscounts(CarDealerDbContext context)
	{
	    var salesDiscounts = context.Sales.Select(s => new
	    {
		car = new
		{
		    s.Car.Make,
		    s.Car.Model,
		    s.Car.TravelledDistance
		},
		customerName = s.Customer.Name,
		Discount = decimal.Parse(s.Discount.ToString("0.##")),
		price = s.Car.CarParts.Sum(cp => cp.Part.Price),
		priceWithDiscount = s.Car.CarParts.Sum(cp => cp.Part.Price) - s.Car.CarParts.Sum(cp => cp.Part.Price) * s.Discount
	    }).ToArray();
	    string output = JsonConvert.SerializeObject(salesDiscounts, Formatting.Indented);
	    File.WriteAllText(@"..\..\..\Output\sales-discounts.json", output, Encoding.UTF8);
	}
    }
}
