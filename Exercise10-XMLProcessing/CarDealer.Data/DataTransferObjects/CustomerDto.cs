using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace CarDealer.Data.DataTransferObjects
{
    [XmlType("customer")]
    public class CustomerDto
    {
	[Required]
	[MinLength(3)]
	[XmlAttribute("name")]
	public string Name { get; set; }

	[Required]
	[XmlElement("birth-date")]
	public DateTime BirthDate { get; set; }

	[Required]
	[XmlElement("is-young-driver")]
	public bool IsYoungDriver { get; set; }
    }
}
