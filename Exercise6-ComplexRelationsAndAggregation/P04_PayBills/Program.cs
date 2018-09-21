using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using P02_BillsPaymentSystem.Data.Initialization;

namespace P04_PayBills
{
    public class Program
    {
	public static void Main()
	{
	    Console.Write("Please enter user ID to pay bills for: ");
	    #region /* Suggested IDs for testing: */
	    /* 3 => user has 2 bank accounts with positive balances
	     *	    and 2 credit cards that have not expired
	     */
	    #endregion
	    int userId = int.Parse(Console.ReadLine());
	    using (var database = new BillsPaymentSystemContext())
	    {
		Initializer.Seed(database);
		User user = FindUser(database, userId);
		Console.WriteLine($"User found: {user.FirstName} {user.LastName}");
		Console.Write("Please enter the sum that has to be paid: ");
		#region /* Suggested amounts for testing with userId = 3: */
		/* 4999 => withdraw from bank account №1 without emptying it
		 * 256000.38 => empty bank account №1 then withdraw from bank account №2 without emptying it
		 * 598464.7 => empty both bank accounts and reach the exact limit of credit card №1
		 * 650000 => empty both bank accounts and credit card №1 then withdraw from credit card №2 without reaching its limit
		 * 698463.5 => empty all bank accounts and credit cards
		 * 698463.51 => Insufficient funds
		 */
		#endregion
		decimal amount = decimal.Parse(Console.ReadLine());
		try
		{
		    PayBills(user, amount);
		    Console.WriteLine("__________________________________________________");
		    Console.WriteLine("Bills successfully paid. Status after transaction:");
		    Console.WriteLine("──────────────────────────────────────────────────");
		    PrintUserInfo(user);
		}
		catch (InvalidOperationException exception)
		{
		    Console.WriteLine(exception.Message);
		}
	    }
	}

	private static User FindUser(BillsPaymentSystemContext context, int userId)
	{
	    User user = null;
	    while (user == null)
	    {
		user = context.Users.Where(u => u.UserId == userId)
		    .Include(u => u.PaymentMethods).ThenInclude(pm => pm.BankAccount)
		    .Include(u => u.PaymentMethods).ThenInclude(pm => pm.CreditCard)
		    .FirstOrDefault();
		if (user == null)
		{
		    Console.WriteLine($"User with ID {userId} not found!");
		    Console.Write("Please enter a different user ID to pay bills for: ");
		    userId = int.Parse(Console.ReadLine());
		}
	    }
	    return user;
	}

	private static void PayBills(User user, decimal amount)
	{
	    BankAccount[] bankAccountsWithPositiveBalance = user.PaymentMethods
		.Where(pm => pm.BankAccountId != null && pm.BankAccount.Balance > 0)
		.Select(pm => pm.BankAccount).ToArray();
	    decimal moneyInBankAccounts = bankAccountsWithPositiveBalance.Sum(ba => ba.Balance);
	    CreditCard[] creditCardsWithLimitLeft = user.PaymentMethods
		.Where(pm => pm.CreditCardId != null && pm.CreditCard.LimitLeft > 0)
		.Select(pm => pm.CreditCard).ToArray();
	    decimal limitLeftOnCreditCards = creditCardsWithLimitLeft.Sum(cc => cc.LimitLeft);
	    if (amount > moneyInBankAccounts + limitLeftOnCreditCards)
		throw new InvalidOperationException("Insufficient funds!");
	    foreach (BankAccount bankAccount in bankAccountsWithPositiveBalance
		.OrderBy(ba => ba.BankAccountId))
	    {
		if (amount <= bankAccount.Balance)
		{
		    bankAccount.Withdraw(amount);
		    amount = 0;
		    break;
		}
		else
		{
		    amount -= bankAccount.Balance;
		    bankAccount.Withdraw(bankAccount.Balance);
		}
	    }
	    if (amount == 0) return;
	    foreach (CreditCard creditCard in creditCardsWithLimitLeft
		.OrderBy(cc => cc.CreditCardId))
	    {
		if (amount <= creditCard.LimitLeft)
		{
		    creditCard.Withdraw(amount);
		    amount = 0;
		    break;
		}
		else
		{
		    amount -= creditCard.LimitLeft;
		    creditCard.Withdraw(creditCard.LimitLeft);
		}
	    }
	}

	private static void PrintUserInfo(User user)
	{
	    StringBuilder userInfo = new StringBuilder();
	    userInfo.AppendLine($"User: {user.FirstName} {user.LastName}");
	    if (user.PaymentMethods.Any(pm => pm.BankAccountId != null))
	    {
		BankAccount[] bankAccounts = user.PaymentMethods
		    .Where(pm => pm.BankAccount != null)
		    .Select(pm => pm.BankAccount).ToArray();
		userInfo.AppendLine("Bank Accounts:");
		foreach (BankAccount bankAccount in bankAccounts)
		{
		    userInfo.AppendLine($"-- ID: {bankAccount.BankAccountId}");
		    userInfo.AppendLine($"--- Balance: {bankAccount.Balance:F2}");
		    userInfo.AppendLine($"--- Bank: {bankAccount.BankName}");
		    userInfo.AppendLine($"--- SWIFT: {bankAccount.SwiftCode}");
		}
	    }
	    if (user.PaymentMethods.Any(pm => pm.CreditCardId != null))
	    {
		CreditCard[] creditCards = user.PaymentMethods
		    .Where(pm => pm.CreditCard != null)
		    .Select(pm => pm.CreditCard).ToArray();
		userInfo.AppendLine("Credit Cards:");
		foreach (CreditCard creditCard in creditCards)
		{
		    userInfo.AppendLine($"-- ID: {creditCard.CreditCardId}");
		    userInfo.AppendLine($"--- Limit: {creditCard.Limit:F2}");
		    userInfo.AppendLine($"--- Money Owed: {creditCard.MoneyOwed:F2}");
		    userInfo.AppendLine($"--- Expiration Date: {creditCard.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
		}
	    }
	    Console.WriteLine(userInfo.ToString().TrimEnd());
	}
    }
}
