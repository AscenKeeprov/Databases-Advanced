using System;
using P01_BillsPaymentSystem.Data.Models;

namespace P02_BillsPaymentSystem.Data.Initialization
{
    public class CreditCardGenerator
    {
	private static Random rng = new Random();

	public static CreditCard[] GenerateCreditCards()
	{
	    CreditCard[] creditCards = new CreditCard[]
	    {
		new CreditCard() { Limit = 25000M, ExpirationDate = DateTime.Now.AddDays(227)},
		new CreditCard() { Limit = 5000M, ExpirationDate = DateTime.Now.AddDays(502)},
		new CreditCard() { Limit = 10000M, ExpirationDate = DateTime.Now.AddDays(-201)},
		new CreditCard() { Limit = 2500M, ExpirationDate = DateTime.Now.AddDays(149)},
		new CreditCard() { Limit = 5000M, ExpirationDate = DateTime.Now.AddDays(-183)},
		new CreditCard() { Limit = 100000M, ExpirationDate = DateTime.Now.AddYears(3)},
	    };
	    decimal[] creditCardDebts = new decimal[]
	    {
		1720.30M,
		350M,
		0.001337M,
		67.80M,
		0.001337M,
		1.20M
	    };
	    for (int i = 0; i < creditCards.Length; i++)
	    {
		if (i < creditCardDebts.Length)
		{
		    creditCards[i].Withdraw(creditCardDebts[i]);
		    if (creditCards[i].ExpirationDate < DateTime.Now)
			creditCards[i].Deposit(creditCardDebts[i]);
		}
		else
		{
		    decimal debt = new decimal(rng.Next(), rng.Next(), rng.Next(), false, 25);
		    while (debt > creditCards[i].Limit)
		    {
			debt = new decimal(rng.Next(), rng.Next(), rng.Next(), false, 25);
		    }
		    debt = decimal.Parse(String.Format("{0:0.##}", debt));
		    creditCards[i].Withdraw(debt);
		    if (creditCards[i].ExpirationDate < DateTime.Now)
			creditCards[i].Deposit(debt);
		}
	    }
	    return creditCards;
	}
    }
}
