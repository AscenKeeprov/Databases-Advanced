using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductShop.Models
{
    public class Product
    {
	public Product()
	{
	    ProductCategories = new HashSet<CategoryProduct>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MinLength(3)]
	public string Name { get; set; }

	[Required]
	public decimal Price { get; set; }

	public int? BuyerId { get; set; }
	public virtual User Buyer { get; set; }

	public int SellerId { get; set; }
	public virtual User Seller { get; set; }

	public virtual ICollection<CategoryProduct> ProductCategories { get; set; }
    }
}
