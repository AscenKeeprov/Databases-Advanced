using CarDealer.Data.EntityConfiguration;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Data
{
    public class CarDealerDbContext : DbContext
    {
	public CarDealerDbContext() { }

	public CarDealerDbContext(DbContextOptions options) : base(options) { }

	public virtual DbSet<Car> Cars { get; set; }
	public virtual DbSet<Customer> Customers { get; set; }
	public virtual DbSet<PartCar> PartCars { get; set; }
	public virtual DbSet<Part> Parts { get; set; }
	public virtual DbSet<Sale> Sales { get; set; }
	public virtual DbSet<Supplier> Suppliers { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
	    if (!optionsBuilder.IsConfigured)
	    {
		optionsBuilder.UseSqlServer(CarDealerDbConfiguration.ConnectionString);
	    }
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	    modelBuilder.ApplyConfiguration(new CarConfiguration());
	    modelBuilder.ApplyConfiguration(new CustomerConfiguration());
	    modelBuilder.ApplyConfiguration(new PartCarConfiguration());
	    modelBuilder.ApplyConfiguration(new PartConfiguration());
	    modelBuilder.ApplyConfiguration(new SaleConfiguration());
	    modelBuilder.ApplyConfiguration(new SupplierConfiguration());
	}
    }
}
