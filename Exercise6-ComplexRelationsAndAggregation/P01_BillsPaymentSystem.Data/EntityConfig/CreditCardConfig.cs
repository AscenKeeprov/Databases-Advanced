using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class CreditCardConfig : IEntityTypeConfiguration<CreditCard>
    {
	public void Configure(EntityTypeBuilder<CreditCard> entityBuilder)
	{
	    entityBuilder.HasOne(cc => cc.PaymentMethod)
		.WithOne(pm => pm.CreditCard)
		.HasForeignKey<PaymentMethod>(pm => pm.Id);
	}
    }
}
