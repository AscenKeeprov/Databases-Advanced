using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    public class BetConfig : IEntityTypeConfiguration<Bet>
    {
	public void Configure(EntityTypeBuilder<Bet> entityBuilder)
	{
	    entityBuilder.ToTable("Bets");

	    entityBuilder.HasKey(b => b.BetId);

	    entityBuilder.Property(b => b.Amount)
		.IsRequired(true);

	    entityBuilder.Property(b => b.Prediction)
		.IsRequired(true);

	    entityBuilder.Property(b => b.DateTime)
		.IsRequired(true);

	    entityBuilder.Property(b => b.UserId)
		.IsRequired(true);

	    entityBuilder.Property(b => b.GameId)
		.IsRequired(true);

	    entityBuilder.HasOne(b => b.Game)
		.WithMany(g => g.Bets)
		.HasForeignKey(b => b.GameId);

	    entityBuilder.HasOne(b => b.User)
		.WithMany(u => u.Bets)
		.HasForeignKey(b => b.UserId);
	}
    }
}
