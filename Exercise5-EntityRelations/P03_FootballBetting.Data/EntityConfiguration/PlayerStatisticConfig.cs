using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    public class PlayerStatisticConfig : IEntityTypeConfiguration<PlayerStatistic>
    {
	public void Configure(EntityTypeBuilder<PlayerStatistic> entityBuilder)
	{
	    entityBuilder.ToTable("PlayerStatistics");

	    entityBuilder.HasKey(ps => new { ps.PlayerId, ps.GameId });

	    entityBuilder.Property(ps => ps.PlayerId)
		.IsRequired(true);

	    entityBuilder.Property(ps => ps.GameId)
		.IsRequired(true);

	    entityBuilder.HasOne(ps => ps.Player)
		.WithMany(p => p.PlayerStatistics)
		.HasForeignKey(ps => ps.PlayerId);

	    entityBuilder.HasOne(ps => ps.Game)
		.WithMany(g => g.PlayerStatistics)
		.HasForeignKey(ps => ps.GameId);
	}
    }
}
