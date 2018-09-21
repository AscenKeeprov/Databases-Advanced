using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Data.DataTransferObjects;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.App
{
    public class StartUp
    {
	static void Main()
	{
	    var mapperConfiguration = new MapperConfiguration(cfg
		=> cfg.AddProfile<CarDealerProfile>());
	    IMapper mapper = mapperConfiguration.CreateMapper();
	    using (var context = new CarDealerDbContext())
	    {
		#region /* Uncomment and run to initialize database and recreate missing output files*/
		//InitializeDatabase(context, mapper);
		//GetCarsWithDistance(context);
		//GetFerrariCars(context);
		//GetLocalSuppliers(context);
		//GetCarsParts(context);
		//GetTotalSalesByCustomer(context, mapper);
		//GetDiscountedSales(context);
		#endregion
	    }
	}

	private static void InitializeDatabase(CarDealerDbContext context, IMapper mapper)
	{
	    context.Database.Migrate();
	    ImportSuppliers(context, mapper);
	    ImportCustomers(context, mapper);
	    ImportParts(context, mapper);
	    ImportCars(context, mapper);
	    SeedSales(context, mapper);
	}

	private static void ImportSuppliers(CarDealerDbContext context, IMapper mapper)
	{
	    using (var suppliersXml = new StreamReader(@"..\..\..\Resources\suppliers.xml"))
	    {
		var serializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("suppliers"));
		var supplierDtos = (SupplierDto[])serializer.Deserialize(suppliersXml);
		HashSet<Supplier> suppliers = context.Suppliers.ToHashSet();
		for (int i = 0; i < supplierDtos.Length; i++)
		{
		    SupplierDto supplierDto = supplierDtos[i];
		    if (IsObjectValid(supplierDto))
		    {
			Supplier supplier = mapper.Map<Supplier>(supplierDto);
			if (!suppliers.Any(s => s.Name == supplier.Name))
			{
			    context.Suppliers.Add(supplier);
			    suppliers.Add(supplier);
			}
		    }
		}
		context.SaveChanges();
	    }
	}

	private static void ImportCustomers(CarDealerDbContext context, IMapper mapper)
	{
	    using (var customersXml = new StreamReader(@"..\..\..\Resources\customers.xml"))
	    {
		var serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("customers"));
		var customerDtos = (CustomerDto[])serializer.Deserialize(customersXml);
		HashSet<Customer> customers = context.Customers.ToHashSet();
		for (int i = 0; i < customerDtos.Length; i++)
		{
		    CustomerDto customerDto = customerDtos[i];
		    if (IsObjectValid(customerDto))
		    {
			Customer customer = mapper.Map<Customer>(customerDto);
			if (!customers.Any(c => c.Name == customer.Name && c.BirthDate == customer.BirthDate))
			{
			    context.Customers.Add(customer);
			    customers.Add(customer);
			}
		    }
		}
		context.SaveChanges();
	    }
	}

	private static void ImportParts(CarDealerDbContext context, IMapper mapper)
	{
	    if (!context.Suppliers.Any()) ImportSuppliers(context, mapper);
	    using (var partsXml = new StreamReader(@"..\..\..\Resources\parts.xml"))
	    {
		var serializer = new XmlSerializer(typeof(PartDto[]), new XmlRootAttribute("parts"));
		var partDtos = (PartDto[])serializer.Deserialize(partsXml);
		HashSet<Part> parts = context.Parts.ToHashSet();
		for (int i = 0; i < partDtos.Length; i++)
		{
		    PartDto partDto = partDtos[i];
		    if (IsObjectValid(partDto))
		    {
			Part part = mapper.Map<Part>(partDto);
			Random rng = new Random();
			int[] supplierIds = context.Suppliers.Select(s => s.Id).ToArray();
			part.Supplier_Id = rng.Next(supplierIds.Min(), supplierIds.Max());
			if (!parts.Any(p => p.Name == part.Name))
			{
			    context.Parts.Add(part);
			    parts.Add(part);
			}
		    }
		}
		context.SaveChanges();
	    }
	}

	private static void ImportCars(CarDealerDbContext context, IMapper mapper)
	{
	    if (!context.Parts.Any()) ImportParts(context, mapper);
	    using (var carsXml = new StreamReader(@"..\..\..\Resources\cars.xml"))
	    {
		var serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("cars"));
		var carDtos = (CarDto[])serializer.Deserialize(carsXml);
		HashSet<Car> cars = context.Cars.ToHashSet();
		for (int i = 0; i < carDtos.Length; i++)
		{
		    CarDto carDto = carDtos[i];
		    if (IsObjectValid(carDto))
		    {
			Car car = mapper.Map<Car>(carDto);
			Random rng = new Random();
			int carPartsCount = rng.Next(10, 21);
			int[] partIds = context.Parts.Select(p => p.Id).ToArray();
			for (int j = 0; j < carPartsCount; j++)
			{
			    int partId = rng.Next(partIds.Min(), partIds.Max());
			    while (car.CarParts.Any(p => p.Part_Id == partId))
			    {
				partId = rng.Next(partIds.Min(), partIds.Max());
			    }
			    PartCar carPart = new PartCar()
			    {
				Part_Id = partId,
				Car_Id = car.Id
			    };
			    car.CarParts.Add(carPart);
			}
			if (!cars.Any(c => c.Make == car.Make && c.Model == car.Model
					&& c.TravelledDistance == car.TravelledDistance))
			{
			    context.Cars.Add(car);
			    cars.Add(car);
			}
		    }
		}
		context.SaveChanges();
	    }
	}

	private static void SeedSales(CarDealerDbContext context, IMapper mapper)
	{
	    if (!context.Customers.Any()) ImportCustomers(context, mapper);
	    if (!context.Cars.Any()) ImportCars(context, mapper);
	    var customers = context.Customers.Select(c => new { c.Id, c.IsYoungDriver }).ToArray();
	    int[] customerIds = customers.Select(c => c.Id).ToArray();
	    int[] carIds = context.Cars.Select(c => c.Id).ToArray();
	    decimal[] discounts = new decimal[] { 0M, 0.05M, 0.1M, 0.15M, 0.2M, 0.3M, 0.4M, 0.5M };
	    Random rng = new Random();
	    HashSet<Sale> sales = context.Sales.ToHashSet();
	    int carsForSale = carIds.Length * 100 / 110;
	    while (sales.Count < carsForSale)
	    {
		int customerId = rng.Next(customerIds.Min(), customerIds.Max());
		var customer = customers.First(c => c.Id == customerId);
		int carId = rng.Next(carIds.Min(), carIds.Max());
		int discountIndex = rng.Next(0, discounts.Length);
		decimal discount = discounts[discountIndex];
		if (customer.IsYoungDriver) discount += 0.05M;
		Sale sale = new Sale()
		{
		    Car_Id = carId,
		    Customer_Id = customerId,
		    Discount = discount
		};
		if (!sales.Any(s => s.Car_Id == sale.Car_Id))
		{
		    context.Sales.Add(sale);
		    sales.Add(sale);
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

	private static void GetCarsWithDistance(CarDealerDbContext context)
	{
	    CarDto[] carsDistance2M = context.Cars
		.Where(c => c.TravelledDistance > 2000000)
		.OrderBy(c => c.Make).ThenBy(c => c.Model)
		.ThenByDescending(c => c.TravelledDistance)
		.Select(c => new CarDto()
		{
		    Make = c.Make,
		    Model = c.Model,
		    TravelledDistance = c.TravelledDistance
		}).ToArray();
	    var serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
	    var serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("cars"));
	    using (StreamWriter writer = new StreamWriter(@"..\..\..\Output\cars-distance-2M.xml", false))
	    {
		serializer.Serialize(writer, carsDistance2M, serializerNamespaces);
	    }
	}

	private static void GetFerrariCars(CarDealerDbContext context)
	{
	    CarIdModDistDto[] ferrariCars = context.Cars
		.Where(c => c.Make.Equals("Ferrari"))
		.OrderBy(c => c.Model).ThenByDescending(c => c.TravelledDistance)
		.Select(c => new CarIdModDistDto()
		{
		    Id = c.Id,
		    Model = c.Model,
		    DistanceTravelled = c.TravelledDistance
		}).ToArray();
	    var serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
	    var serializer = new XmlSerializer(typeof(CarIdModDistDto[]), new XmlRootAttribute("cars"));
	    using (StreamWriter writer = new StreamWriter(@"..\..\..\Output\ferrari-cars.xml", false))
	    {
		serializer.Serialize(writer, ferrariCars, serializerNamespaces);
	    }
	}

	private static void GetLocalSuppliers(CarDealerDbContext context)
	{
	    SupplierLocalDto[] localSuppliers = context.Suppliers
		.Where(s => s.IsImporter == false)
		.Select(s => new SupplierLocalDto()
		{
		    Id = s.Id,
		    Name = s.Name,
		    PartsSuppliedCount = context.Parts
			.Where(p => p.Supplier.Id == s.Id).Count()
		}).ToArray();
	    var serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
	    var serializer = new XmlSerializer(typeof(SupplierLocalDto[]), new XmlRootAttribute("suppliers"));
	    using (StreamWriter writer = new StreamWriter(@"..\..\..\Output\local-suppliers.xml", false))
	    {
		serializer.Serialize(writer, localSuppliers, serializerNamespaces);
	    }
	}

	private static void GetCarsParts(CarDealerDbContext context)
	{
	    CarPartsDto[] carsParts = context.Cars
		.Select(c => new CarPartsDto()
		{
		    Make = c.Make,
		    Model = c.Model,
		    DistanceTravelled = c.TravelledDistance,
		    Parts = c.CarParts.Select(p => new PartPriceDto()
		    {
			Name = p.Part.Name,
			Price = p.Part.Price
		    }).ToArray()
		}).ToArray();
	    var serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
	    var serializer = new XmlSerializer(typeof(CarPartsDto[]), new XmlRootAttribute("cars"));
	    using (StreamWriter writer = new StreamWriter(@"..\..\..\Output\cars-and-parts.xml", false))
	    {
		serializer.Serialize(writer, carsParts, serializerNamespaces);
	    }
	}

	private static void GetTotalSalesByCustomer(CarDealerDbContext context, IMapper mapper)
	{
	    CustomerExpenditureDto[] customersExpenditures = context.Customers
		.Include(c => c.CarPurchases)
		.ThenInclude(s => s.Car)
		.ThenInclude(c => c.CarParts)
		.ThenInclude(cp => cp.Part)
		.Where(c => c.CarPurchases.Count >= 1)
		.Select(c => mapper.Map<CustomerExpenditureDto>(c))
		.ToArray();
	    #region /* TODO: Find out how to apply discounts while using direct projection */
	    //   .Select(c => new CustomerExpenditureDto()
	    //{
	    //    FullName = c.Name,
	    //    CarsBought = c.CarPurchases.Count,
	    //    MoneySpent = c.CarPurchases.Select(s => s.Car).Select(car => car.CarParts.Sum(cp => cp.Part.Price)).Sum()
	    //})
	    #endregion
	    var serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
	    var serializer = new XmlSerializer(typeof(CustomerExpenditureDto[]), new XmlRootAttribute("customers"));
	    using (StreamWriter writer = new StreamWriter(@"..\..\..\Output\customers-total-sales.xml", false))
	    {
		serializer.Serialize(writer, customersExpenditures, serializerNamespaces);
	    }
	}

	private static void GetDiscountedSales(CarDealerDbContext context)
	{
	    SaleDiscountDto[] salesDiscounts = context.Sales
		.Select(s => new SaleDiscountDto()
		{
		    Car = new CarAttributesDto()
		    {
			Make = s.Car.Make,
			Model = s.Car.Model,
			DistanceTravelled = s.Car.TravelledDistance
		    },
		    CustomerName = s.Customer.Name,
		    Discount = decimal.Parse(s.Discount.ToString("0.##")),
		    Price = s.Car.CarParts.Sum(cp => cp.Part.Price),
		    DiscountedPrice = s.Car.CarParts.Sum(cp => cp.Part.Price) - s.Car.CarParts.Sum(cp => cp.Part.Price) * s.Discount
		}).ToArray();
	    var serializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
	    var serializer = new XmlSerializer(typeof(SaleDiscountDto[]), new XmlRootAttribute("sales"));
	    using (StreamWriter writer = new StreamWriter(@"..\..\..\Output\sales-discounts.xml", false))
	    {
		serializer.Serialize(writer, salesDiscounts, serializerNamespaces);
	    }
	}
    }
}
