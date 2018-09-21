using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.EntityConfiguration
{
    public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
	public void Configure(EntityTypeBuilder<Invitation> entityBuilder)
	{
	    entityBuilder.HasKey(i => i.Id);

	    entityBuilder.HasOne(i => i.Team)
		.WithMany(t => t.InvitationsSent)
		.HasForeignKey(i => i.TeamId)
		.OnDelete(DeleteBehavior.Restrict);

	    entityBuilder.HasOne(i => i.InvitedUser)
		.WithMany(u => u.InvitationsReceived)
		.HasForeignKey(i => i.InvitedUserId)
		.OnDelete(DeleteBehavior.Restrict);
	}
    }
}
