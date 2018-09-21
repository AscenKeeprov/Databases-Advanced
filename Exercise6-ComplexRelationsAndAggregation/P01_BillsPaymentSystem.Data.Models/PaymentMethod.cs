using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P01_BillsPaymentSystem.Data.Models.Attributes;
using P01_BillsPaymentSystem.Data.Models.Enumerations;

namespace P01_BillsPaymentSystem.Data.Models
{
    [Table(nameof(PaymentMethod) + "s")]
    public class PaymentMethod
    {
	[Key]
	public int Id { get; set; }

	[Required]
	[Range(0, 1)]
	public PaymentMethodType Type { get; set; }

	[Required]
	[ForeignKey(nameof(User))]
	public int UserId { get; set; }

	[XORNULL(nameof(CreditCardId))]
	[ForeignKey(nameof(BankAccount))]
	public int? BankAccountId { get; set; }

	[ForeignKey(nameof(CreditCard))]
	public int? CreditCardId { get; set; }

	public virtual User User { get; set; }
	public virtual BankAccount BankAccount { get; set; }
	public virtual CreditCard CreditCard { get; set; }
    }
}
