using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PetClinic.Models
{
    public class Procedure
    {
	public Procedure()
	{
	    ProcedureAnimalAids = new HashSet<ProcedureAnimalAid>();
	}

	[Key]
	public int Id { get; set; }

	[ForeignKey(nameof(Animal))]
	public int AnimalId { get; set; }
	[Required]
	public virtual Animal Animal { get; set; }

	[ForeignKey(nameof(Vet))]
	public int VetId { get; set; }
	[Required]
	public virtual Vet Vet { get; set; }

	[Required]
	public DateTime DateTime { get; set; }

	[NotMapped]
	public decimal Cost => ProcedureAnimalAids.Sum(paa => paa.AnimalAid.Price);

	public virtual ICollection<ProcedureAnimalAid> ProcedureAnimalAids { get; set; }
    }
}
