using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.EntityConfiguration
{
    public class PassportConfiguration : IEntityTypeConfiguration<Passport>
    {
	public void Configure(EntityTypeBuilder<Passport> entityBuilder)
	{
	    entityBuilder.HasKey(p => p.SerialNumber);

	    entityBuilder.Property(p => p.OwnerPhoneNumber)
		.IsRequired(true);

	    entityBuilder.Property(p => p.OwnerName)
		.HasMaxLength(30)
		.IsRequired(true);

	    entityBuilder.Property(p => p.RegistrationDate)
		.IsRequired(true);

	    entityBuilder.HasOne(p => p.Animal)
		.WithOne(a => a.Passport)
		.HasPrincipalKey<Passport>(p => p.SerialNumber);
	}
    }
}
