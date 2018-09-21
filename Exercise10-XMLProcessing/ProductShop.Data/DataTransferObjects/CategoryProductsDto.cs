using System.Xml.Serialization;

namespace ProductShop.Data.DataTransferObjects
{
    [XmlType("category")]
    public class CategoryProductsDto
    {
	[XmlAttribute("name")]
	public string Name { get; set; }

	[XmlElement("products-count")]
	public int ProductsCount { get; set; }

	[XmlElement("average-price")]
	public string ProductsAveragePrice { get; set; }

	[XmlElement("total-revenue")]
	public string TotalRevenue { get; set; }
    }
}
