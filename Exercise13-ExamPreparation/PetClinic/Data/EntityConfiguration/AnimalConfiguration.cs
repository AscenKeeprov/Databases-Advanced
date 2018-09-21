using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.EntityConfiguration
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
	public void Configure(EntityTypeBuilder<Animal> entityBuilder)
	{
	    entityBuilder.HasKey(a => a.Id);

	    entityBuilder.Property(a => a.Name)
		.HasMaxLength(20)
		.IsRequired(true);

	    entityBuilder.Property(a => a.Type)
		.HasMaxLength(20)
		.IsRequired(true);

	    entityBuilder.Property(a => a.Age)
		.IsRequired(true);

	    entityBuilder.HasOne(a => a.Passport)
		.WithOne(p => p.Animal)
		.HasForeignKey<Animal>(a => a.PassportSerialNumber);

	    entityBuilder.HasMany(a => a.Procedures)
		.WithOne(p => p.Animal)
		.HasForeignKey(p => p.AnimalId);
	}
    }
}
