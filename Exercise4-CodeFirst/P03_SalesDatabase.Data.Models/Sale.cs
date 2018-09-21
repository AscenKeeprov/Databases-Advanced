namespace P03_SalesDatabase.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Sales")]
    public class Sale
    {
	[Key]
	public int SaleId { get; set; }

	[Column(TypeName = "DATETIME2")]
	public DateTime Date { get; set; }

	public int ProductId { get; set; }
	public Product Product { get; set; }

	public int CustomerId { get; set; }
	public Customer Customer { get; set; }

	public int StoreId { get; set; }
	public Store Store { get; set; }
    }
}
