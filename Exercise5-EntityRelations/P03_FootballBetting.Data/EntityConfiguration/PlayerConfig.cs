using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    public class PlayerConfig : IEntityTypeConfiguration<Player>
    {
	public void Configure(EntityTypeBuilder<Player> entityBuilder)
	{
	    entityBuilder.ToTable("Players");

	    entityBuilder.HasKey(p => p.PlayerId);

	    entityBuilder.Property(p => p.Name)
		.HasMaxLength(100)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(p => p.SquadNumber)
		.IsRequired(true);

	    entityBuilder.Property(p => p.IsInjured)
		.IsRequired(true);

	    entityBuilder.Property(p => p.TeamId)
		.IsRequired(true);

	    entityBuilder.Property(p => p.PositionId)
		.IsRequired(true);

	    entityBuilder.HasOne(p => p.Team)
		.WithMany(t => t.Players)
		.HasForeignKey(p => p.TeamId);

	    entityBuilder.HasOne(p => p.Position)
		.WithMany(pos => pos.Players)
		.HasForeignKey(p => p.PositionId);

	    entityBuilder.HasMany(p => p.PlayerStatistics)
		.WithOne(ps => ps.Player)
		.HasForeignKey(ps => ps.PlayerId);
	}
    }
}
