using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace P01_BillsPaymentSystem.Data.Models
{
    [Table(nameof(BankAccount) + "s")]
    public class BankAccount
    {
	private const string BankAccountMinBalance = "0";
	private const string BankAccountMaxBalance = "100000000000000000";
	private const string ZeroOrNegativeMoneyError = "Invalid sum specified! Please enter a positive amount of money to {0}.";

	[Key]
	public int BankAccountId { get; set; }

	[Required]
	[Column(TypeName = "DECIMAL(18,2)")]
	[Range(typeof(decimal), BankAccountMinBalance, BankAccountMaxBalance)]
	public decimal Balance { get; private set; }

	[Required]
	[MaxLength(50)]
	[Column(TypeName = "NVARCHAR(50)")]
	public string BankName { get; set; }

	[Required]
	[MaxLength(20)]
	[Column(TypeName = "VARCHAR(20)")]
	public string SwiftCode { get; set; }

	public virtual PaymentMethod PaymentMethod { get; set; }

	public void Deposit(decimal amount)
	{
	    if (amount <= 0) throw new ArgumentOutOfRangeException(String.Format(
		ZeroOrNegativeMoneyError, MethodBase.GetCurrentMethod().Name.ToLower()));
	    Balance += amount;
	}

	public void Withdraw(decimal amount)
	{
	    if (amount <= 0) throw new ArgumentOutOfRangeException(String.Format(
		ZeroOrNegativeMoneyError, MethodBase.GetCurrentMethod().Name.ToLower()));
	    Balance -= Math.Min(amount, Balance);
	}
    }
}
