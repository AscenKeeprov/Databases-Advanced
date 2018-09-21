using System.Xml.Serialization;

namespace ProductShop.Data.DataTransferObjects
{
    [XmlRoot("users")]
    public class UsersDto
    {
	[XmlAttribute("count")]
	public int UsersCount { get; set; }

	[XmlElement("user")]
	public UserSoldProductsDto[] UsersProducts { get; set; }
    }
}
