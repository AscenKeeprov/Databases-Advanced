using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.EntityConfiguration
{
    public class VetConfiguration : IEntityTypeConfiguration<Vet>
    {
	public void Configure(EntityTypeBuilder<Vet> entityBuilder)
	{
	    entityBuilder.HasKey(v => v.Id);

	    entityBuilder.Property(v => v.Name)
		.HasMaxLength(40)
		.IsRequired(true);

	    entityBuilder.Property(v => v.Profession)
		.HasMaxLength(50)
		.IsRequired(true);

	    entityBuilder.Property(v => v.Age)
		.IsRequired(true);

	    entityBuilder.Property(v => v.PhoneNumber)
		.IsRequired(true);

	    entityBuilder.HasIndex(v => v.PhoneNumber)
		.IsUnique(true);

	    entityBuilder.HasMany(v => v.Procedures)
		.WithOne(p => p.Vet)
		.HasForeignKey(p => p.VetId);
	}
    }
}
