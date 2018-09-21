using System.Xml.Serialization;

namespace PetClinic.Data.DataTransferObjects
{
    [XmlType("Procedure")]
    public class ProcedureExportDto
    {
	[XmlElement("Passport")]
	public string PassportSerialNumber { get; set; }

	[XmlElement("OwnerNumber")]
	public string OwnerNumber { get; set; }

	[XmlElement("DateTime")]
	public string DateTime { get; set; }

	[XmlArray("AnimalAids")]
	[XmlArrayItem("AnimalAid")]
	public AnimalAidDto[] AnimalAids { get; set; }

	[XmlElement("TotalPrice")]
	public decimal TotalPrice { get; set; }
    }
}
