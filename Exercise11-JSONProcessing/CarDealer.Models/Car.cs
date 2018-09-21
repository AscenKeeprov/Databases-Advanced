using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace CarDealer.Models
{
    public class Car
    {
	public Car()
	{
	    CarParts = new HashSet<PartCar>();
	    CarSales = new HashSet<Sale>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MinLength(3)]
	public string Make { get; set; }

	[Required]
	[MinLength(2)]
	public string Model { get; set; }

	public long TravelledDistance { get; set; }

	public virtual ICollection<PartCar> CarParts { get; set; }
	public virtual ICollection<Sale> CarSales { get; set; }
    }
}
