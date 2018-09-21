using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace CarDealer.Data.DataTransferObjects
{
    [XmlType("supplier")]
    public class SupplierDto
    {
	[Required]
	[MinLength(3)]
	[XmlAttribute("name")]
	public string Name { get; set; }

	[Required]
	[XmlAttribute("is-importer")]
	public bool IsImporter { get; set; }
    }
}
