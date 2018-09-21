using System.Linq;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using P02_BillsPaymentSystem.Data.Validation;

namespace P02_BillsPaymentSystem.Data.Initialization
{
    public class Initializer
    {
	public static void Seed(BillsPaymentSystemContext context)
	{
	    context.Database.Migrate();
	    if (!IsDatabaseEmpty(context)) ReseedDatabase(context);
	    else
	    {
		SeedUsers(context);
		SeedBankAccounts(context);
		SeedCreditCards(context);
		SeedPaymentMethods(context);
	    }
	}

	private static void ReseedDatabase(BillsPaymentSystemContext context)
	{
	    context.PaymentMethods.RemoveRange(context.PaymentMethods);
	    context.CreditCards.RemoveRange(context.CreditCards);
	    context.BankAccounts.RemoveRange(context.BankAccounts);
	    context.Users.RemoveRange(context.Users);
	    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ([Users], RESEED, 0)");
	    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ([BankAccounts], RESEED, 0)");
	    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ([CreditCards], RESEED, 0)");
	    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ([PaymentMethods], RESEED, 0)");
	    context.SaveChanges();
	    using (var database = new BillsPaymentSystemContext())
	    {
		Seed(database);
	    }
	}

	private static bool IsDatabaseEmpty(BillsPaymentSystemContext context)
	{
	    bool isUsersEmpty = !context.Users.ToArray().Any();
	    bool isBankAccountsEmpty = !context.BankAccounts.ToArray().Any();
	    bool isCreditCardsEmpty = !context.CreditCards.ToArray().Any();
	    bool isPaymentMethodsEmpty = !context.PaymentMethods.ToArray().Any();
	    bool isDatabaseEmpty = isUsersEmpty && isBankAccountsEmpty
		&& isCreditCardsEmpty && isPaymentMethodsEmpty;
	    return isDatabaseEmpty;
	}

	private static void SeedUsers(BillsPaymentSystemContext database)
	{
	    User[] users = UserGenerator.GenerateUsers();
	    for (int i = 0; i < users.Length; i++)
	    {
		if (Validater.IsEntityValid(users[i]))
		    database.Users.Add(users[i]);
	    }
	    database.SaveChanges();
	}

	private static void SeedBankAccounts(BillsPaymentSystemContext database)
	{
	    BankAccount[] bankAccounts = BankAccountGenerator.GenerateBankAccounts();
	    for (int i = 0; i < bankAccounts.Length; i++)
	    {
		if (Validater.IsEntityValid(bankAccounts[i]))
		    database.BankAccounts.Add(bankAccounts[i]);
	    }
	    database.SaveChanges();
	}

	private static void SeedCreditCards(BillsPaymentSystemContext database)
	{
	    CreditCard[] creditCards = CreditCardGenerator.GenerateCreditCards();
	    for (int i = 0; i < creditCards.Length; i++)
	    {
		if (Validater.IsEntityValid(creditCards[i]))
		    database.CreditCards.Add(creditCards[i]);
	    }
	    database.SaveChanges();
	}

	private static void SeedPaymentMethods(BillsPaymentSystemContext database)
	{
	    PaymentMethod[] paymentMethods = PaymentMethodGenerator.GeneratePaymentMethods();
	    for (int i = 0; i < paymentMethods.Length; i++)
	    {
		if (Validater.IsEntityValid(paymentMethods[i]))
		    database.PaymentMethods.Add(paymentMethods[i]);
	    }
	    database.SaveChanges();
	}
    }
}
