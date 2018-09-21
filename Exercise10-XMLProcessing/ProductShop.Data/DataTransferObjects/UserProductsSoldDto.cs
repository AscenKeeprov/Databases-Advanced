using System.Xml.Serialization;

namespace ProductShop.Data.DataTransferObjects
{
    [XmlType("user")]
    public class UserProductsSoldDto
    {
	[XmlAttribute("first-name")]
	public string FirstName { get; set; }

	[XmlAttribute("last-name")]
	public string LastName { get; set; }

	[XmlArray("sold-products")]
	[XmlArrayItem("product", typeof(ProductDto))]
	public ProductDto[] ProductsSold { get; set; }
    }
}
