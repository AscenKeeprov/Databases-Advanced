using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
	public void Configure(EntityTypeBuilder<User> entityBuilder)
	{
	    entityBuilder.ToTable("Users");

	    entityBuilder.HasKey(u => u.UserId);

	    entityBuilder.Property(u => u.Username)
		.HasMaxLength(25)
		.IsUnicode(false)
		.IsRequired(true);

	    entityBuilder.HasIndex(u => u.Username)
		.IsUnique(true);

	    entityBuilder.Property(u => u.Password)
		.HasMaxLength(25)
		.IsUnicode(false)
		.IsRequired(true);

	    entityBuilder.Property(u => u.Email)
		.IsUnicode(false)
		.IsRequired(true);

	    entityBuilder.Property(u => u.Name)
		.HasMaxLength(100)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.HasMany(u => u.Bets)
		.WithOne(b => b.User)
		.HasForeignKey(b => b.UserId);
	}
    }
}
