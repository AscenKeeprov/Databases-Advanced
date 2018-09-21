using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace P01_BillsPaymentSystem.Data.Models
{
    [Table(nameof(CreditCard) + "s")]
    public class CreditCard
    {
	private const string CreditCardMinLimit = "500";
	private const string CreditCardMaxLimit = "100000";
	private const string CreditCardMinDebt = "0";
	private const string CreditCardMaxDebt = "999999999999999999";
	private const string ZeroOrNegativeMoneyError = "Invalid sum specified! Please enter a positive amount of money to {0}.";

	[Key]
	public int CreditCardId { get; set; }

	[Required]
	[Column(TypeName = "DECIMAL(8,2)")]
	[Range(typeof(decimal), CreditCardMinLimit, CreditCardMaxLimit)]
	public decimal Limit { get; set; }

	[Required]
	[Column(TypeName = "DECIMAL(18,2)")]
	[Range(typeof(decimal), CreditCardMinDebt, CreditCardMaxDebt)]
	public decimal MoneyOwed { get; private set; }

	[NotMapped]
	public decimal LimitLeft => Math.Max(Limit - MoneyOwed, 0M);

	[Required]
	[Column(TypeName = "DATE")]
	public DateTime ExpirationDate { get; set; }

	public virtual PaymentMethod PaymentMethod { get; set; }

	public void Deposit(decimal amount)
	{
	    if (amount <= 0) throw new ArgumentOutOfRangeException(String.Format(
		ZeroOrNegativeMoneyError, MethodBase.GetCurrentMethod().Name.ToLower()));
	    MoneyOwed -= amount;
	}

	public void Withdraw(decimal amount)
	{
	    if (amount <= 0) throw new ArgumentOutOfRangeException(String.Format(
		ZeroOrNegativeMoneyError, MethodBase.GetCurrentMethod().Name.ToLower()));
	    MoneyOwed += Math.Min(amount, LimitLeft);
	}
    }
}
