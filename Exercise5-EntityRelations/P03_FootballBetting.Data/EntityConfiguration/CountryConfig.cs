using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
	public void Configure(EntityTypeBuilder<Country> entityBuilder)
	{
	    entityBuilder.ToTable("Countries");

	    entityBuilder.HasKey(c => c.CountryId);

	    entityBuilder.Property(c => c.Name)
		.HasMaxLength(100)
		.IsUnicode(false)
		.IsRequired(true);

	    entityBuilder.HasMany(c => c.Towns)
		.WithOne(t => t.Country)
		.HasForeignKey(t => t.CountryId);
	}
    }
}
