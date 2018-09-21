using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.EntityConfiguration
{
    public class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
	public void Configure(EntityTypeBuilder<UserTeam> entityBuilder)
	{
	    entityBuilder.HasKey(ut => new { ut.UserId, ut.TeamId });

	    entityBuilder.HasOne(ut => ut.User)
		.WithMany(u => u.TeamsJoined)
		.HasForeignKey(ut => ut.UserId)
		.OnDelete(DeleteBehavior.Restrict);

	    entityBuilder.HasOne(ut => ut.Team)
		.WithMany(t => t.Members)
		.HasForeignKey(ut => ut.TeamId)
		.OnDelete(DeleteBehavior.Restrict);
	}
    }
}
