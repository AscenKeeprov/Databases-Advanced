using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.EntityConfiguration
{
    public class EventTeamConfiguration : IEntityTypeConfiguration<EventTeam>
    {
	public void Configure(EntityTypeBuilder<EventTeam> entityBuilder)
	{
	    entityBuilder.HasKey(et => new { et.TeamId, et.EventId });

	    entityBuilder.HasOne(et => et.Team)
		.WithMany(t => t.EventsAttended)
		.HasForeignKey(et => et.TeamId)
		.OnDelete(DeleteBehavior.Restrict);

	    entityBuilder.HasOne(et => et.Event)
		.WithMany(e => e.TeamsParticipating)
		.HasForeignKey(et => et.EventId)
		.OnDelete(DeleteBehavior.Restrict);
	}
    }
}
