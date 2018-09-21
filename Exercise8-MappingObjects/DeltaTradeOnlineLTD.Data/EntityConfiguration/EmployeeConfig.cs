using DeltaTradeOnlineLTD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeltaTradeOnlineLTD.Data.EntityConfiguration
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
	public void Configure(EntityTypeBuilder<Employee> entityBuilder)
	{
	    entityBuilder.HasKey(e => e.EmployeeId);

	    entityBuilder.Property(e => e.FirstName)
		.IsRequired(true);

	    entityBuilder.Property(e => e.LastName)
		.IsRequired(true);

	    entityBuilder.Property(e => e.Salary)
		.IsRequired(true);

	    entityBuilder.HasOne(e => e.Manager)
		.WithMany(m => m.Subordinates)
		.HasForeignKey(e => e.ManagerId);
	}
    }
}
