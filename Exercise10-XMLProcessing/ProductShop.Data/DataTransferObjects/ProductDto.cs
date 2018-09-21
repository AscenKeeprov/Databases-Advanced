using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ProductShop.Data.DataTransferObjects
{
    [XmlType("product")]
    public class ProductDto
    {
	[MinLength(3)]
	[XmlElement("name")]
	public string Name { get; set; }

	[Range(typeof(decimal), "0", "79228162514264337593543950335")]
	[XmlElement("price")]
	public decimal Price { get; set; }
    }
}
