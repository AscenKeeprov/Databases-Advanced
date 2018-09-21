using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductShop.Models
{
    public class Category
    {
	public Category()
	{
	    CategoryProducts = new HashSet<CategoryProduct>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(maximumLength: 15, MinimumLength = 3)]
	public string Name { get; set; }

	public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
