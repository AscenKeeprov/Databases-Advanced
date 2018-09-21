using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.EntityConfiguration
{
    class BookConfiguration : IEntityTypeConfiguration<Book>
    {
	public void Configure(EntityTypeBuilder<Book> entityBuilder)
	{
	    entityBuilder.HasKey(b => b.BookId);

	    entityBuilder.Property(b => b.Title)
		.IsRequired(true)
		.IsUnicode(true)
		.HasMaxLength(50);

	    entityBuilder.Property(b => b.Description)
		.IsRequired(true)
		.IsUnicode(true)
		.HasMaxLength(1000);

	    entityBuilder.Property(b => b.ReleaseDate)
		.IsRequired(false);

	    entityBuilder.HasOne(b => b.Author)
		.WithMany(a => a.Books)
		.HasForeignKey(b => b.AuthorId);
	}
    }
}
