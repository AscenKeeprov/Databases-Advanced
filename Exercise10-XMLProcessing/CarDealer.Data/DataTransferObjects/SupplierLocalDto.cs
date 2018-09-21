using System.Xml.Serialization;

namespace CarDealer.Data.DataTransferObjects
{
    [XmlType("supplier")]
    public class SupplierLocalDto
    {
	[XmlAttribute("id")]
	public int Id { get; set; }

	[XmlAttribute("name")]
	public string Name { get; set; }

	[XmlAttribute("parts-count")]
	public int PartsSuppliedCount { get; set; }
    }
}
