using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;
using TeamBuilder.Utilities;

namespace TeamBuilder.Data.EntityConfiguration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
	public void Configure(EntityTypeBuilder<Event> entityBuilder)
	{
	    entityBuilder.HasKey(e => e.Id);

	    entityBuilder.Property(e => e.Name)
		.HasMaxLength(Constants.EventNameMaxLength)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(e => e.Description)
		.HasMaxLength(Constants.EventDescriptionMaxLength)
		.IsUnicode(true);

	    entityBuilder.HasOne(e => e.Creator)
		.WithMany(u => u.EventsCreated)
		.HasForeignKey(e => e.CreatorId)
		.OnDelete(DeleteBehavior.Restrict);

	    entityBuilder.HasMany(e => e.TeamsParticipating)
		.WithOne(et => et.Event)
		.HasForeignKey(et => et.EventId);
	}
    }
}
