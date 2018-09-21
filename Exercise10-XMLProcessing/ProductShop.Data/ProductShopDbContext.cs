using Microsoft.EntityFrameworkCore;
using ProductShop.Data.EntityConfiguration;
using ProductShop.Models;

namespace ProductShop.Data
{
    public class ProductShopDbContext : DbContext
    {
	public ProductShopDbContext() { }

	public ProductShopDbContext(DbContextOptions options) : base(options) { }

	public ProductShopDbContext(DbContextOptions<ProductShopDbContext> options)
	    : base(options) { }

	public virtual DbSet<Category> Categories { get; set; }
	public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }
	public virtual DbSet<Product> Products { get; set; }
	public virtual DbSet<User> Users { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
	    if (!optionsBuilder.IsConfigured)
	    {
		optionsBuilder.UseSqlServer(ProductShopDbConfiguration.ConnectionString);
	    }
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	    modelBuilder.ApplyConfiguration(new CategoryConfiguration());
	    modelBuilder.ApplyConfiguration(new CategoryProductConfiguration());
	    modelBuilder.ApplyConfiguration(new ProductConfiguration());
	    modelBuilder.ApplyConfiguration(new UserConfiguration());
	}
    }
}
