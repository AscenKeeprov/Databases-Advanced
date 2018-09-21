using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
	public void Configure(EntityTypeBuilder<Game> entityBuilder)
	{
	    entityBuilder.ToTable("Games");

	    entityBuilder.HasKey(g => g.GameId);

	    entityBuilder.Property(g => g.DateTime)
		.IsRequired(true);

	    entityBuilder.HasOne(g => g.HomeTeam)
		.WithMany(t => t.HomeGames)
		.HasForeignKey(g => g.HomeTeamId)
		.OnDelete(DeleteBehavior.ClientSetNull);

	    entityBuilder.HasOne(g => g.AwayTeam)
		.WithMany(t => t.AwayGames)
		.HasForeignKey(g => g.AwayTeamId)
		.OnDelete(DeleteBehavior.ClientSetNull);

	    entityBuilder.HasMany(g => g.PlayerStatistics)
		.WithOne(ps => ps.Game)
		.HasForeignKey(ps => ps.GameId);

	    entityBuilder.HasMany(g => g.Bets)
		.WithOne(b => b.Game)
		.HasForeignKey(b => b.GameId);
	}
    }
}
