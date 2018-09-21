using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;
using TeamBuilder.Utilities;

namespace TeamBuilder.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
	public void Configure(EntityTypeBuilder<User> entityBuilder)
	{
	    entityBuilder.HasKey(u => u.Id);

	    entityBuilder.Property(u => u.Username)
		.HasMaxLength(Constants.UserUsernameMaxLength)
		.IsUnicode(false)
		.IsRequired(true);

	    entityBuilder.HasIndex(u => new { u.Username, u.Id })
		.IsUnique(true);

	    entityBuilder.Property(u => u.FirstName)
		.HasMaxLength(Constants.UserPersonalNameMaxLength);

	    entityBuilder.Property(u => u.LastName)
		.HasMaxLength(Constants.UserPersonalNameMaxLength);

	    entityBuilder.Property(u => u.Password)
		.HasMaxLength(Constants.PasswordMaxLength)
		.IsUnicode(false)
		.IsRequired(true);

	    entityBuilder.HasMany(u => u.TeamsCreated)
		.WithOne(t => t.Creator)
		.HasForeignKey(t => t.CreatorId);

	    entityBuilder.HasMany(u => u.InvitationsReceived)
		.WithOne(i => i.InvitedUser)
		.HasForeignKey(i => i.InvitedUserId);

	    entityBuilder.HasMany(u => u.TeamsJoined)
		.WithOne(ut => ut.User)
		.HasForeignKey(ut => ut.UserId);

	    entityBuilder.HasMany(u => u.EventsCreated)
		.WithOne(e => e.Creator)
		.HasForeignKey(e => e.CreatorId);
	}
    }
}
