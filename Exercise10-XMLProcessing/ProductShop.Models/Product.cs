using System.Collections.Generic;

namespace ProductShop.Models
{
    public class Product
    {
	public Product()
	{
	    ProductCategories = new HashSet<CategoryProduct>();
	}

	public int Id { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }

	public int? BuyerId { get; set; }
	public User Buyer { get; set; }

	public int SellerId { get; set; }
	public User Seller { get; set; }

	public virtual ICollection<CategoryProduct> ProductCategories { get; set; }
    }
}
