using Microsoft.EntityFrameworkCore;
using BookShop.Models;
using BookShop.Data.EntityConfiguration;

namespace BookShop.Data
{
    public class BookShopContext : DbContext
    {
	public BookShopContext() { }

	public BookShopContext(DbContextOptions options) : base(options) { }

	public virtual DbSet<Author> Authors { get; set; }
	public virtual DbSet<Book> Books { get; set; }
	public virtual DbSet<Category> Categories { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
	    if (!optionsBuilder.IsConfigured)
	    {
		optionsBuilder.UseSqlServer(Configuration.ConnectionString);
	    }
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	    modelBuilder.ApplyConfiguration(new AuthorConfiguration());
	    modelBuilder.ApplyConfiguration(new BookConfiguration());
	    modelBuilder.ApplyConfiguration(new CategoryConfiguration());
	    modelBuilder.ApplyConfiguration(new BookCategoryConfiguration());
	}
    }
}
