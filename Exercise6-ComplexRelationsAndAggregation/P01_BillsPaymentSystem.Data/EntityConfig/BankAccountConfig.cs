using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
	public void Configure(EntityTypeBuilder<BankAccount> entityBuilder)
	{
	    entityBuilder.HasOne(ba => ba.PaymentMethod)
		.WithOne(pm => pm.BankAccount)
		.HasForeignKey<PaymentMethod>(pm => pm.Id);
	}
    }
}
