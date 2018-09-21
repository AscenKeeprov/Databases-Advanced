using System.Xml.Serialization;

namespace ProductShop.Data.DataTransferObjects
{
    [XmlType("product")]
    public class ProductPriceDto
    {
	[XmlAttribute("name")]
	public string Name { get; set; }

	[XmlAttribute("price")]
	public decimal Price { get; set; }
    }
}
