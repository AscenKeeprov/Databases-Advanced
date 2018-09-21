using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CarDealer.Models
{
    public class Customer
    {
	public Customer()
	{
	    Purchases = new HashSet<Sale>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MinLength(3)]
	public string Name { get; set; }

	[Required]
	public DateTime BirthDate { get; set; }

	[Required]
	public bool IsYoungDriver { get; set; }

	[JsonProperty("Sales")]
	public virtual ICollection<Sale> Purchases { get; set; }
    }
}
