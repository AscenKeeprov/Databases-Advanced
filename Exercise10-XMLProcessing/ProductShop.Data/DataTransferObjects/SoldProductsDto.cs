using System.Xml.Serialization;

namespace ProductShop.Data.DataTransferObjects
{
    [XmlType("sold-products")]
    public class SoldProductsDto
    {
	[XmlAttribute("count")]
	public int ProductsCounts { get; set; }

	[XmlElement("product")]
	public ProductPriceDto[] Products { get; set; }
    }
}
