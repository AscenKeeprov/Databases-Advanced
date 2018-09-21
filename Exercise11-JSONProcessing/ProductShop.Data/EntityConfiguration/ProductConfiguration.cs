using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductShop.Models;

namespace ProductShop.Data.EntityConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
	public void Configure(EntityTypeBuilder<Product> entityBuilder)
	{
	    entityBuilder.HasKey(p => p.Id);

	    entityBuilder.Property(p => p.Name)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(p => p.Price)
		.IsRequired(true);

	    entityBuilder.HasOne(p => p.Seller)
		.WithMany(s => s.ProductsSold)
		.HasForeignKey(p => p.SellerId);

	    entityBuilder.HasOne(p => p.Buyer)
		.WithMany(s => s.ProductsBought)
		.HasForeignKey(p => p.BuyerId);

	    entityBuilder.HasMany(p => p.ProductCategories)
		.WithOne(pc => pc.Product)
		.HasForeignKey(pc => pc.ProductId);
	}
    }
}
