namespace PhotoShare.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PhotoShare.Models;

    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
	public void Configure(EntityTypeBuilder<Tag> builder)
	{
	    builder.HasKey(e => e.Id);

	    builder.Property(e => e.Name)
		   .IsRequired(true)
		   .HasMaxLength(50);
	}
    }
}
