namespace P03_SalesDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_SalesDatabase.Data.Models;

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
	public void Configure(EntityTypeBuilder<Product> builder)
	{
	    builder.ToTable("Products");

	    builder.HasKey(p => p.ProductId);

	    builder.Property(p => p.Name)
		.HasMaxLength(50)
		.IsUnicode(true);

	    builder.Property(p => p.Description)
		.HasMaxLength(250)
		.HasDefaultValue("No description")
		.IsUnicode(true);

	    builder.HasMany(p => p.Sales)
		.WithOne(s => s.Product)
		.HasForeignKey(s => s.ProductId);
	}
    }
}
