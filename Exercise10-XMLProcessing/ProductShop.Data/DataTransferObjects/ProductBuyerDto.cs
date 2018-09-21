using System.Xml.Serialization;

namespace ProductShop.Data.DataTransferObjects
{
    [XmlType("product")]
    public class ProductBuyerDto
    {
	[XmlAttribute("name")]
	public string Name { get; set; }

	[XmlAttribute("price")]
	public decimal Price { get; set; }

	[XmlAttribute("buyer")]
	public string Buyer { get; set; }
    }
}
