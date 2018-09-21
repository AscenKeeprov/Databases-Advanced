using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.EntityConfiguration
{
    class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
	public void Configure(EntityTypeBuilder<Author> entityBuilder)
	{
	    entityBuilder.HasKey(a => a.AuthorId);

	    entityBuilder.Property(a => a.FirstName)
		.IsRequired(false)
		.IsUnicode(true)
		.HasMaxLength(50);

	    entityBuilder.Property(e => e.LastName)
		.IsRequired(true)
		.IsUnicode(true)
		.HasMaxLength(50);
	}
    }
}
