using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data.Models.Enumerations;

namespace P02_BillsPaymentSystem.Data.Initialization
{
    public class PaymentMethodGenerator
    {
	public static PaymentMethod[] GeneratePaymentMethods()
	{
	    PaymentMethod[] paymentMethods = new PaymentMethod[]
	    {
		new PaymentMethod() { Type = PaymentMethodType.BankAccount, UserId = 1, BankAccountId = 1 },
		new PaymentMethod() { Type = PaymentMethodType.BankAccount, UserId = 1, BankAccountId = 5 },
		new PaymentMethod() { Type = PaymentMethodType.CreditCard, UserId = 2, CreditCardId = 3 },
		new PaymentMethod() { Type = PaymentMethodType.CreditCard, UserId = 2, CreditCardId = 4 },
		new PaymentMethod() { Type = PaymentMethodType.BankAccount, UserId = 3, BankAccountId = 6 },
		new PaymentMethod() { Type = PaymentMethodType.BankAccount, UserId = 3, BankAccountId = 7 },
		new PaymentMethod() { Type = PaymentMethodType.CreditCard, UserId = 3, CreditCardId = 1 },
		new PaymentMethod() { Type = PaymentMethodType.CreditCard, UserId = 3, CreditCardId = 6 },
		new PaymentMethod() { Type = PaymentMethodType.BankAccount, UserId = 4, BankAccountId = 4 },
		new PaymentMethod() { Type = PaymentMethodType.CreditCard, UserId = 4, CreditCardId = 5 },
		new PaymentMethod() { Type = PaymentMethodType.BankAccount, UserId = 5, BankAccountId = 2 },
		new PaymentMethod() { Type = PaymentMethodType.CreditCard, UserId = 5, CreditCardId = 2 },
		new PaymentMethod() { Type = PaymentMethodType.BankAccount, UserId = 6, BankAccountId = 3 },
		new PaymentMethod() { Type = PaymentMethodType.BankAccount, UserId = 6, BankAccountId = 8 },
	    };
	    return paymentMethods;
	}
    }
}
