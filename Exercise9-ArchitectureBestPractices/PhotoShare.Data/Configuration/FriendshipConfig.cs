﻿namespace PhotoShare.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PhotoShare.Models;

    public class FriendshipConfig : IEntityTypeConfiguration<Friendship>
    {
	public void Configure(EntityTypeBuilder<Friendship> builder)
	{
	    builder.HasKey(e => new { e.UserId, e.FriendId });

	    builder.HasOne(e => e.User)
		   .WithMany(u => u.FriendsAdded)
		   .HasForeignKey(e => e.UserId)
		   .OnDelete(DeleteBehavior.Restrict);
	}
    }
}
