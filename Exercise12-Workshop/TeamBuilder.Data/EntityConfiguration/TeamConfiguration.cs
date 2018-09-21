using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;
using TeamBuilder.Utilities;

namespace TeamBuilder.Data.EntityConfiguration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
	public void Configure(EntityTypeBuilder<Team> entityBuilder)
	{
	    entityBuilder.HasKey(t => t.Id);

	    entityBuilder.Property(t => t.Name)
		.HasMaxLength(Constants.TeamNameMaxLength)
		.IsRequired(true);

	    entityBuilder.Property(t => t.Description)
		.HasMaxLength(Constants.TeamDescriptionMaxLength);

	    entityBuilder.Property(t => t.Acronym)
		.HasMaxLength(Constants.TeamAcronymLength)
		.IsFixedLength(true)
		.IsUnicode(false)
		.IsRequired(true);

	    entityBuilder.HasIndex(t => new { t.Name, t.Acronym, t.Id })
		.IsUnique(true);

	    entityBuilder.HasOne(t => t.Creator)
		.WithMany(u => u.TeamsCreated)
		.HasForeignKey(t => t.CreatorId)
		.OnDelete(DeleteBehavior.Restrict);

	    entityBuilder.HasMany(t => t.InvitationsSent)
		.WithOne(i => i.Team)
		.HasForeignKey(i => i.TeamId);

	    entityBuilder.HasMany(t => t.Members)
		.WithOne(ut => ut.Team)
		.HasForeignKey(ut => ut.TeamId);

	    entityBuilder.HasMany(t => t.EventsAttended)
		.WithOne(et => et.Team)
		.HasForeignKey(et => et.TeamId);
	}
    }
}
