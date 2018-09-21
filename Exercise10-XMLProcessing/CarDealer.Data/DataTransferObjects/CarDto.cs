using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace CarDealer.Data.DataTransferObjects
{
    [XmlType("car")]
    public class CarDto
    {
	[Required]
	[XmlElement("make")]
	public string Make { get; set; }

	[Required]
	[XmlElement("model")]
	public string Model { get; set; }

	[Required]
	[XmlElement("travelled-distance")]
	public long TravelledDistance { get; set; }
    }
}
