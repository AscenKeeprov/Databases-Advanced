using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace CarDealer.Data.DataTransferObjects
{
    [XmlType("part")]
    public class PartDto
    {
	[Required]
	[MinLength(3)]
	[XmlAttribute("name")]
	public string Name { get; set; }

	[Required]
	[Range(typeof(decimal), "0", "79228162514264337593543950335")]
	[XmlAttribute("price")]
	public decimal Price { get; set; }

	[Required]
	[Range(typeof(int), "1", "1000")]
	[XmlAttribute("quantity")]
	public int Quantity { get; set; }
    }
}
