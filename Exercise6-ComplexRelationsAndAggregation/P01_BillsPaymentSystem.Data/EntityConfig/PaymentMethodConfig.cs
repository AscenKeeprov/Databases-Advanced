using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
	public void Configure(EntityTypeBuilder<PaymentMethod> entityBuilder)
	{
	    entityBuilder.HasIndex(pm => new { pm.UserId, pm.BankAccountId, pm.CreditCardId })
	    	.IsUnique(true)
		.HasFilter(null);

	    entityBuilder.HasIndex(pm => pm.BankAccountId)
	    	.IsUnique(true)
		.HasFilter("[BankAccountId] IS NOT NULL AND [CreditCardId] IS NULL");

	    entityBuilder.HasIndex(pm => pm.CreditCardId)
	    	.IsUnique(true)
		.HasFilter("[CreditCardId] IS NOT NULL AND [BankAccountId] IS NULL");

	    entityBuilder.HasOne(pm => pm.BankAccount)
		.WithOne(ba => ba.PaymentMethod)
		.HasForeignKey<PaymentMethod>(pm => pm.BankAccountId);

	    entityBuilder.HasOne(pm => pm.CreditCard)
		.WithOne(cc => cc.PaymentMethod)
		.HasForeignKey<PaymentMethod>(pm => pm.CreditCardId);
	}
    }
}
