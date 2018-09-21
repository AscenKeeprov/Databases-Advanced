using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    public class ColorConfig : IEntityTypeConfiguration<Color>
    {
	public void Configure(EntityTypeBuilder<Color> entityBuilder)
	{
	    entityBuilder.ToTable("Colors");

	    entityBuilder.HasKey(c => c.ColorId);

	    entityBuilder.Property(c => c.Name)
		.HasMaxLength(25)
		.IsUnicode(false)
		.IsRequired(true);

	    entityBuilder.HasMany(c => c.PrimaryKitTeams)
		.WithOne(t => t.PrimaryKitColor)
		.HasForeignKey(t => t.PrimaryKitColorId);

	    entityBuilder.HasMany(c => c.SecondaryKitTeams)
		.WithOne(t => t.SecondaryKitColor)
		.HasForeignKey(t => t.SecondaryKitColorId);
	}
    }
}
