using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.EntityConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
	public void Configure(EntityTypeBuilder<Customer> entityBuilder)
	{
	    entityBuilder.HasKey(c => c.Id);

	    entityBuilder.Property(c => c.Name)
		.IsUnicode(true)
		.IsRequired(true);

	    entityBuilder.Property(c => c.BirthDate)
		.IsRequired(true);

	    entityBuilder.Property(c => c.IsYoungDriver)
		.IsRequired(true);

	    entityBuilder.HasMany(c => c.Purchases)
		.WithOne(s => s.Customer)
		.HasForeignKey(s => s.Customer_Id);
	}
    }
}
