using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.EntityConfiguration
{
    public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
	public void Configure(EntityTypeBuilder<Procedure> entityBuilder)
	{
	    entityBuilder.HasKey(p => p.Id);

	    entityBuilder.Property(p => p.DateTime)
		.IsRequired(true);

	    entityBuilder.Ignore(p => p.Cost);

	    entityBuilder.HasOne(p => p.Animal)
		.WithMany(a => a.Procedures)
		.HasForeignKey(p => p.AnimalId);

	    entityBuilder.HasMany(p => p.ProcedureAnimalAids)
		.WithOne(paa => paa.Procedure)
		.HasForeignKey(paa => paa.ProcedureId);
	}
    }
}
