using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.EntityConfiguration
{
    class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
	public void Configure(EntityTypeBuilder<BookCategory> entityBuilder)
	{
	    entityBuilder.HasKey(bc => new { bc.BookId, bc.CategoryId });

	    entityBuilder.HasOne(bc => bc.Category)
		.WithMany(c => c.CategoryBooks)
		.HasForeignKey(bc => bc.CategoryId);

	    entityBuilder.HasOne(bc => bc.Book)
		.WithMany(b => b.BookCategories)
		.HasForeignKey(bc => bc.BookId);
	}
    }
}
