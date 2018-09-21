using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using P02_BillsPaymentSystem.Data.Initialization;

namespace P03_UserDetails
{
    public class Program
    {
	public static void Main()
	{
	    Console.Write("Please enter user ID to search for: ");
	    #region /* Suggested IDs for testing: */
	    /* 1 => user has no credit cards
	     * 2 => user has no bank accounts
	     * 3 => user has bank accounts and credit cards
	     */
	    #endregion
	    int userId = int.Parse(Console.ReadLine());
	    using (var database = new BillsPaymentSystemContext())
	    {
		Initializer.Seed(database);
		User user = FindUser(database, userId);
		PrintUserInfo(user);
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
		    Console.Write("Please enter a different user ID to search for: ");
		    userId = int.Parse(Console.ReadLine());
		}
	    }
	    return user;
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
