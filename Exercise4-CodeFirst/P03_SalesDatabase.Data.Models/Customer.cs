using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_SalesDatabase.Data.Models
{
    [Table("Customers")]
    public class Customer
    {
	[Key]
	public int CustomerId { get; set; }

	[Column(TypeName = "NVARCHAR(100)")]
	[MaxLength(100)]
	public string Name { get; set; }

	[Column(TypeName = "VARCHAR(80)")]
	[MaxLength(80)]
	public string Email { get; set; }

	public string CreditCardNumber { get; set; }

	public ICollection<Sale> Sales { get; set; }
    }
}
