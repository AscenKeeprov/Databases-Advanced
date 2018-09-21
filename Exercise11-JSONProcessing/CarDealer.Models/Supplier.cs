using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class Supplier
    {
	public Supplier()
	{
	    PartsSupplied = new HashSet<Part>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MinLength(3)]
	public string Name { get; set; }

	[Required]
	public bool IsImporter { get; set; }

	public virtual ICollection<Part> PartsSupplied { get; set; }
    }
}
