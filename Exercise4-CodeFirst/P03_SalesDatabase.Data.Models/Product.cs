namespace P03_SalesDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Products")]
    public class Product : IComparable<Product>
    {
	[Key]
	public int ProductId { get; set; }

	[Column(TypeName = "NVARCHAR(50)")]
	[MaxLength(50)]
	public string Name { get; set; }

	[Column(TypeName = "DECIMAL(18,2)")]
	[Range(typeof(decimal), "1", "10")]
	public decimal Quantity { get; set; }

	[Column(TypeName = "SMALLMONEY")]
	public decimal Price { get; set; }

	[Column(TypeName = "NVARCHAR(250)")]
	[MaxLength(250)]
	public string Description { get; set; }

	public ICollection<Sale> Sales { get; set; }

	public int CompareTo(Product other)
	{
	    return Name.CompareTo(other.Name);
	}

	public override string ToString()
	{
	    return $"{Name} (x{Quantity}) @{Price}";
	}
    }
}
