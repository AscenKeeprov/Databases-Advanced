using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    public class TownConfig : IEntityTypeConfiguration<Town>
    {
	public void Configure(EntityTypeBuilder<Town> entityBuilder)
	{
	    entityBuilder.ToTable("Towns");

	    entityBuilder.HasKey(t => t.TownId);

	    entityBuilder.Property(t => t.Name)
		.HasMaxLength(50)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(t => t.CountryId)
		.IsRequired(true);

	    entityBuilder.HasOne(t => t.Country)
		.WithMany(c => c.Towns)
		.HasForeignKey(t => t.CountryId);

	    entityBuilder.HasMany(town => town.Teams)
		.WithOne(team => team.Town)
		.HasForeignKey(team => team.TownId);
	}
    }
}
