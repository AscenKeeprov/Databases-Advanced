using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CarDealer.Models
{
    public class Car
    {
	public Car()
	{
	    CarParts = new HashSet<PartCar>();
	    CarSales = new HashSet<Sale>();
	}

	public int Id { get; set; }
	public string Make { get; set; }
	public string Model { get; set; }
	public long TravelledDistance { get; set; }

	[NotMapped]
	public decimal Price => CarParts.Select(cp => cp.Part).Sum(p => p.Price);

	public virtual ICollection<PartCar> CarParts { get; set; }
	public virtual ICollection<Sale> CarSales { get; set; }
    }
}
