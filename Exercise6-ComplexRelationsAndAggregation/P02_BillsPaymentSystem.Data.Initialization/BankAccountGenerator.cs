using System;
using P01_BillsPaymentSystem.Data.Models;

namespace P02_BillsPaymentSystem.Data.Initialization
{
    public class BankAccountGenerator
    {
	private static Random rng = new Random();

	public static BankAccount[] GenerateBankAccounts()
	{
	    BankAccount[] bankAccounts = new BankAccount[]
	    {
		new BankAccount() { BankName = "ДСК", SwiftCode = "STSABGSF"},
		new BankAccount() { BankName = "UniCredito Italiano S.p.A.", SwiftCode = "UNCRITM1B84"},
		new BankAccount() { BankName = "Société Générale Expressbank", SwiftCode = "SOGEFRPP"},
		new BankAccount() { BankName = "Bank of East Asia Ltd. (東亞銀行)", SwiftCode = "BEASHKHH"},
		new BankAccount() { BankName = "ДСК", SwiftCode = "STSABGVT"},
		new BankAccount() { BankName = "Raiffeisen Zentralbank Österreich A.G.", SwiftCode = "RZBBBGSF"},
		new BankAccount() { BankName = "Credit Suisse", SwiftCode = "CRESCHZZ"},
		new BankAccount() { BankName = "Société Générale Expressbank", SwiftCode = "SOGEFRPP"}
	    };
	    decimal[] bankAccountBalances = new decimal[]
	    {
		1840.73M,
		6603.55M,
		4092M,
		724019.88M,
		11060.14M,
		5000M,
		570185M,
		1337M
	    };
	    for (int i = 0; i < bankAccounts.Length; i++)
	    {
		if (i < bankAccountBalances.Length)
		{
		    bankAccounts[i].Deposit(bankAccountBalances[i]);
		}
		else
		{
		    decimal balance = new decimal(rng.Next(), rng.Next(), rng.Next(), false, 24);
		    bankAccounts[i].Deposit(decimal.Parse(String.Format("{0:0.##}", balance)));
		}
	    }
	    return bankAccounts;
	}
    }
}
