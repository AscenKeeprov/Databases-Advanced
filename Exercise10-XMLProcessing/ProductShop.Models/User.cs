using System.Collections.Generic;

namespace ProductShop.Models
{
    public class User
    {
	public User()
	{
	    ProductsSold = new HashSet<Product>();
	    ProductsBought = new HashSet<Product>();
	}

	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public int? Age { get; set; }

	public virtual ICollection<Product> ProductsSold { get; set; }
	public virtual ICollection<Product> ProductsBought { get; set; }
    }
}
