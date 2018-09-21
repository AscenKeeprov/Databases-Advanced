using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
	public void Configure(EntityTypeBuilder<Team> entityBuilder)
	{
	    entityBuilder.ToTable("Teams");

	    entityBuilder.HasKey(t => t.TeamId);

	    entityBuilder.Property(t => t.Name)
		.HasMaxLength(50)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(t => t.LogoUrl)
		.IsUnicode(false);

	    entityBuilder.Property(t => t.Initials)
		.HasColumnType("CHAR(3)")
		.IsUnicode(false)
		.IsRequired(true);

	    entityBuilder.Property(t => t.Budget)
		.IsRequired(true);

	    entityBuilder.Property(t => t.TownId)
		.IsRequired(true);

	    entityBuilder.HasOne(t => t.PrimaryKitColor)
		.WithMany(c => c.PrimaryKitTeams)
		.HasForeignKey(t => t.PrimaryKitColorId)
		.OnDelete(DeleteBehavior.ClientSetNull);

	    entityBuilder.HasOne(t => t.SecondaryKitColor)
		.WithMany(c => c.SecondaryKitTeams)
		.HasForeignKey(t => t.SecondaryKitColorId)
		.OnDelete(DeleteBehavior.ClientSetNull);

	    entityBuilder.HasOne(team => team.Town)
		.WithMany(town => town.Teams)
		.HasForeignKey(team => team.TownId);

	    entityBuilder.HasMany(t => t.Players)
		.WithOne(p => p.Team)
		.HasForeignKey(p => p.TeamId);

	    entityBuilder.HasMany(t => t.HomeGames)
		.WithOne(g => g.HomeTeam)
		.HasForeignKey(g => g.HomeTeamId);

	    entityBuilder.HasMany(t => t.AwayGames)
		.WithOne(g => g.AwayTeam)
		.HasForeignKey(g => g.AwayTeamId);
	}
    }
}
