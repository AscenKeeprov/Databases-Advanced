using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.EntityConfiguration
{
    public class AnimalAidConfiguration : IEntityTypeConfiguration<AnimalAid>
    {
	public void Configure(EntityTypeBuilder<AnimalAid> entityBuilder)
	{
	    entityBuilder.HasKey(aa => aa.Id);

	    entityBuilder.Property(aa => aa.Name)
		.HasMaxLength(30)
		.IsRequired(true);

	    entityBuilder.HasIndex(aa => aa.Name)
		.IsUnique(true);

	    entityBuilder.Property(aa => aa.Price)
		.IsRequired(true);

	    entityBuilder.HasMany(aa => aa.AnimalAidProcedures)
		.WithOne(paa => paa.AnimalAid)
		.HasForeignKey(paa => paa.AnimalAidId);
	}
    }
}
