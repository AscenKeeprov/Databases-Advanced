﻿namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data.EntityConfiguration;
    using P03_SalesDatabase.Data.EntityInitialization;
    using P03_SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
	public SalesContext() { }

	public SalesContext(DbContextOptions options) { }

	public DbSet<Product> Products { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Store> Stores { get; set; }
	public DbSet<Sale> Sales { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
	    if (!optionsBuilder.IsConfigured)
		optionsBuilder.UseSqlServer(Configuration.ConnectionString);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	    modelBuilder.ApplyConfiguration(new ProductConfig());
	    modelBuilder.ApplyConfiguration(new CustomerConfig());
	    modelBuilder.ApplyConfiguration(new StoreConfig());
	    modelBuilder.ApplyConfiguration(new SaleConfig());

	    modelBuilder.Entity<Product>().HasData(ProductInit.SeedProducts());
	}
    }
}
