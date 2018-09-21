using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.EntityConfiguration
{
    public class PartCarConfiguration : IEntityTypeConfiguration<PartCar>
    {
	public void Configure(EntityTypeBuilder<PartCar> entityBuilder)
	{
	    entityBuilder.HasKey(pc => new { pc.Part_Id, pc.Car_Id });

	    entityBuilder.HasOne(pc => pc.Part)
		.WithMany(p => p.PartCars)
		.HasForeignKey(pc => pc.Part_Id);

	    entityBuilder.HasOne(pc => pc.Car)
		.WithMany(c => c.CarParts)
		.HasForeignKey(pc => pc.Car_Id);
	}
    }
}
