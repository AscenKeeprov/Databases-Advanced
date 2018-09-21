using System.Xml.Serialization;

namespace CarDealer.Data.DataTransferObjects
{
    [XmlType("car")]
    public class CarPartsDto
    {
	[XmlAttribute("make")]
	public string Make { get; set; }

	[XmlAttribute("model")]
	public string Model { get; set; }

	[XmlAttribute("travelled-distance")]
	public long DistanceTravelled { get; set; }

	[XmlArray("parts")]
	[XmlArrayItem("part", typeof(PartPriceDto))]
	public PartPriceDto[] Parts { get; set; }
    }
}
