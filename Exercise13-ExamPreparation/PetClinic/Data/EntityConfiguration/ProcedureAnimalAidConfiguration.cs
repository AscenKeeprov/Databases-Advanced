using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.EntityConfiguration
{
    public class ProcedureAnimalAidConfiguration : IEntityTypeConfiguration<ProcedureAnimalAid>
    {
	public void Configure(EntityTypeBuilder<ProcedureAnimalAid> entityBuilder)
	{
	    entityBuilder.HasKey(paa => new { paa.ProcedureId, paa.AnimalAidId });

	    entityBuilder.HasOne(paa => paa.Procedure)
		.WithMany(p => p.ProcedureAnimalAids)
		.HasForeignKey(paa => paa.ProcedureId);

	    entityBuilder.HasOne(paa => paa.AnimalAid)
		.WithMany(aa => aa.AnimalAidProcedures)
		.HasForeignKey(paa => paa.AnimalAidId);
	}
    }
}
