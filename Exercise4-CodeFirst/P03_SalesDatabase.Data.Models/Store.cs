namespace P03_SalesDatabase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Stores")]
    public class Store
    {
	[Key]
	public int StoreId { get; set; }

	[Column(TypeName = "NVARCHAR(80)")]
	[MaxLength(80)]
	public string Name { get; set; }

	public ICollection<Sale> Sales { get; set; }
    }
}
