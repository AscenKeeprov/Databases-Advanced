using System.Xml.Serialization;

namespace CarDealer.Data.DataTransferObjects
{
    [XmlType("sale")]
    public class SaleDiscountDto
    {
	[XmlElement("car")]
	public CarAttributesDto Car { get; set; }

	[XmlElement("customer-name")]
	public string CustomerName { get; set; }

	[XmlElement("discount")]
	public decimal Discount { get; set; }

	[XmlElement("price")]
	public decimal Price { get; set; }

	[XmlElement("price-with-discount")]
	public decimal DiscountedPrice { get; set; }
    }
}
