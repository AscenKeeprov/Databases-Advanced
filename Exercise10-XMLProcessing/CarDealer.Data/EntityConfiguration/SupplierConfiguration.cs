using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.EntityConfiguration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
	public void Configure(EntityTypeBuilder<Supplier> entityBuilder)
	{
	    entityBuilder.HasKey(s => s.Id);

	    entityBuilder.Property(s => s.Name)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(s => s.IsImporter)
		.IsRequired(true);

	    entityBuilder.HasMany(s => s.PartsSupplied)
		.WithOne(p => p.Supplier)
		.HasForeignKey(p => p.Supplier_Id);
	}
    }
}
