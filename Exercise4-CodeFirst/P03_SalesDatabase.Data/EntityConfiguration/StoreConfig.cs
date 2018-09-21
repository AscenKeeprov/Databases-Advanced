namespace P03_SalesDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_SalesDatabase.Data.Models;

    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
	public void Configure(EntityTypeBuilder<Store> builder)
	{
	    builder.ToTable("Stores");

	    builder.HasKey(s => s.StoreId);

	    builder.Property(s => s.Name)
		.HasMaxLength(80)
		.IsUnicode(true);

	    builder.HasMany(s => s.Sales)
		.WithOne(s => s.Store)
		.HasForeignKey(s => s.StoreId);
	}
    }
}
