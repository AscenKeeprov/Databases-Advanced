using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductShop.Models;

namespace ProductShop.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
	public void Configure(EntityTypeBuilder<User> entityBuilder)
	{
	    entityBuilder.HasKey(u => u.Id);

	    entityBuilder.Property(u => u.FirstName)
		.IsUnicode(true)
		.IsRequired(false);

	    entityBuilder.Property(u => u.LastName)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(u => u.Age)
		.IsRequired(false);

	    entityBuilder.HasMany(u => u.ProductsSold)
		.WithOne(p => p.Seller)
		.HasForeignKey(p => p.SellerId);

	    entityBuilder.HasMany(u => u.ProductsBought)
		.WithOne(p => p.Buyer)
		.HasForeignKey(p => p.BuyerId);
	}
    }
}
