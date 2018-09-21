using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.EntityConfiguration
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
	public void Configure(EntityTypeBuilder<Sale> entityBuilder)
	{
	    entityBuilder.HasKey(s => s.Id);

	    entityBuilder.Property(s => s.Discount)
		.HasDefaultValue(0);

	    entityBuilder.HasOne(s => s.Customer)
		.WithMany(c => c.CarPurchases)
		.HasForeignKey(s => s.Customer_Id);

	    entityBuilder.HasOne(s => s.Car)
		.WithMany(c => c.CarSales)
		.HasForeignKey(s => s.Car_Id);
	}
    }
}
