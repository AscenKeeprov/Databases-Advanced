using System.Xml.Serialization;

namespace CarDealer.Data.DataTransferObjects
{
    [XmlType("customer")]
    public class CustomerExpenditureDto
    {
	[XmlAttribute("full-name")]
	public string FullName { get; set; }

	[XmlAttribute("bought-cars")]
	public int CarsBought { get; set; }

	[XmlAttribute("spent-money")]
	public decimal MoneySpent { get; set; }
    }
}
