using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    public class PositionConfig : IEntityTypeConfiguration<Position>
    {
	public void Configure(EntityTypeBuilder<Position> entityBuilder)
	{
	    entityBuilder.ToTable("Positions");

	    entityBuilder.HasKey(p => p.PositionId);

	    entityBuilder.Property(p => p.Name)
		.HasMaxLength(25)
		.IsUnicode(false)
		.IsRequired(true);

	    entityBuilder.HasMany(pos => pos.Players)
		.WithOne(p => p.Position)
		.HasForeignKey(p => p.PositionId);
	}
    }
}
