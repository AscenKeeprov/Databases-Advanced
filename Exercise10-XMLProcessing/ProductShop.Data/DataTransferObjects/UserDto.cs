using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ProductShop.Data.DataTransferObjects
{
    [XmlType("user")]
    public class UserDto
    {
	[XmlAttribute("firstName")]
	public string FirstName { get; set; }

	[MinLength(3)]
	[XmlAttribute("lastName")]
	public string LastName { get; set; }

	[Range(0, 150)]
	[XmlAttribute("age")]
	public string Age { get; set; }
    }
}
