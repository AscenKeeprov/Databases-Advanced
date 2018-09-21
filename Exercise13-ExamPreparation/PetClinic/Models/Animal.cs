using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Models
{
    public class Animal
    {
	public Animal()
	{
	    Procedures = new HashSet<Procedure>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(20, MinimumLength = 3)]
	public string Name { get; set; }

	[Required]
	[StringLength(20, MinimumLength = 3)]
	public string Type { get; set; }

	[Required]
	[Range(1, int.MaxValue)]
	public int Age { get; set; }

	[ForeignKey(nameof(Passport))]
	public string PassportSerialNumber { get; set; }
	[Required]
	public virtual Passport Passport { get; set; }

	public virtual ICollection<Procedure> Procedures { get; set; }
    }
}
