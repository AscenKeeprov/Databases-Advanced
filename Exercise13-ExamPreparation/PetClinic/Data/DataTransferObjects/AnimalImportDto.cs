using System.ComponentModel.DataAnnotations;

namespace PetClinic.Data.DataTransferObjects
{
    public class AnimalImportDto
    {
	[Required]
	[StringLength(20, MinimumLength = 3)]
	public string Name { get; set; }

	[Required]
	[StringLength(20, MinimumLength = 3)]
	public string Type { get; set; }

	[Required]
	[Range(1, int.MaxValue)]
	public int Age { get; set; }

	[Required]
	public PassportImportDto Passport { get; set; }
    }
}
