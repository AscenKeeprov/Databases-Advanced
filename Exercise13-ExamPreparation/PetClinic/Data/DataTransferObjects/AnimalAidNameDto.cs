using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace PetClinic.Data.DataTransferObjects
{
    [XmlType("AnimalAid")]
    public class AnimalAidNameDto
    {
	[Required]
	[StringLength(30, MinimumLength = 3)]
	public string Name { get; set; }
    }
}
