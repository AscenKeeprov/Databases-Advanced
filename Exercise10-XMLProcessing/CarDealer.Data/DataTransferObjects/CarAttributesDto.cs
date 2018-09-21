using System.Xml.Serialization;

namespace CarDealer.Data.DataTransferObjects
{
    [XmlType("car")]
    public class CarAttributesDto
    {
	[XmlAttribute("make")]
	public string Make { get; set; }

	[XmlAttribute("model")]
	public string Model { get; set; }

	[XmlAttribute("travelled-distance")]
	public long DistanceTravelled { get; set; }
    }
}
