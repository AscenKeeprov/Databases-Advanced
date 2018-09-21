using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.EntityConfiguration
{
    public class PartConfiguration : IEntityTypeConfiguration<Part>
    {
	public void Configure(EntityTypeBuilder<Part> entityBuilder)
	{
	    entityBuilder.HasKey(p => p.Id);

	    entityBuilder.Property(p => p.Name)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(p => p.Price)
		.IsRequired(true);

	    entityBuilder.Property(p => p.Quantity)
		.IsRequired(true);

	    entityBuilder.HasMany(p => p.PartCars)
		.WithOne(pc => pc.Part)
		.HasForeignKey(pc => pc.Part_Id);

	    entityBuilder.HasOne(p => p.Supplier)
		.WithMany(s => s.PartsSupplied)
		.HasForeignKey(p => p.Supplier_Id);
	}
    }
}
