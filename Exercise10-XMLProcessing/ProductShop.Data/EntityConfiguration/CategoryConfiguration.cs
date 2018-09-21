using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductShop.Models;

namespace ProductShop.Data.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
	public void Configure(EntityTypeBuilder<Category> entityBuilder)
	{
	    entityBuilder.HasKey(c => c.Id);

	    entityBuilder.Property(c => c.Name)
		.HasMaxLength(15)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.HasMany(c => c.CategoryProducts)
		.WithOne(cp => cp.Category)
		.HasForeignKey(cp => cp.CategoryId);
	}
    }
}
