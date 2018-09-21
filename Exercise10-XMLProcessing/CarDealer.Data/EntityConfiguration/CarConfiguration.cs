using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.EntityConfiguration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
	public void Configure(EntityTypeBuilder<Car> entityBuilder)
	{
	    entityBuilder.HasKey(c => c.Id);

	    entityBuilder.Property(c => c.Make)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(c => c.Model)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.HasMany(c => c.CarParts)
		.WithOne(pc => pc.Car)
		.HasForeignKey(pc => pc.Car_Id);

	    entityBuilder.HasMany(c => c.CarSales)
		.WithOne(s => s.Car)
		.HasForeignKey(s => s.Car_Id);
	}
    }
}
