using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace PetClinic.Data.DataTransferObjects
{
    [XmlType("Procedure")]
    public class ProcedureImportDto
    {
	[Required]
	[XmlElement("Vet")]
	[StringLength(40, MinimumLength = 3)]
	public string VetName { get; set; }

	[Required]
	[XmlElement("Animal")]
	[RegularExpression(@"^[a-zA-Z]{7}[0-9]{3}$")]
	public string AnimalPassportSerialNumber { get; set; }

	[Required]
	public string DateTime { get; set; }

	public AnimalAidNameDto[] AnimalAids { get; set; }
    }
}
