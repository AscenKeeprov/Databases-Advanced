using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.EntityConfiguration
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
	public void Configure(EntityTypeBuilder<Category> entityBuilder)
	{
	    entityBuilder.HasKey(c => c.CategoryId);

	    entityBuilder.Property(c => c.Name)
		.IsRequired(true)
		.IsUnicode(true)
		.HasMaxLength(50);
	}
    }
}
