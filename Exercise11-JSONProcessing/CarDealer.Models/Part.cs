using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class Part
    {
	public Part()
	{
	    PartCars = new HashSet<PartCar>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MinLength(3)]
	public string Name { get; set; }

	[Required]
	[Range(typeof(decimal), "0", "79228162514264337593543950335")]
	public decimal Price { get; set; }

	[Required]
	[Range(typeof(int), "1", "1000")]
	public int Quantity { get; set; }

	public int Supplier_Id { get; set; }
	public virtual Supplier Supplier { get; set; }

	public virtual ICollection<PartCar> PartCars { get; set; }
    }
}
