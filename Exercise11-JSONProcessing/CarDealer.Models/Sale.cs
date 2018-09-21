using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CarDealer.Models
{
    public class Sale
    {
	[Key]
	public int Id { get; set; }

	[Range(typeof(decimal), "0", "0.55")]
	public decimal Discount { get; set; }

	public int Car_Id { get; set; }
	public virtual Car Car { get; set; }

	public int Customer_Id { get; set; }
	public virtual Customer Customer { get; set; }
    }
}
