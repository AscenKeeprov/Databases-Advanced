using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductShop.Models;

namespace ProductShop.Data.EntityConfiguration
{
    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
	public void Configure(EntityTypeBuilder<CategoryProduct> entityBuilder)
	{
	    entityBuilder.HasKey(cp => new { cp.CategoryId, cp.ProductId });

	    entityBuilder.HasOne(cp => cp.Category)
		.WithMany(c => c.CategoryProducts)
		.HasForeignKey(cp => cp.CategoryId);

	    entityBuilder.HasOne(cp => cp.Product)
		.WithMany(p => p.ProductCategories)
		.HasForeignKey(cp => cp.ProductId);
	}
    }
}
