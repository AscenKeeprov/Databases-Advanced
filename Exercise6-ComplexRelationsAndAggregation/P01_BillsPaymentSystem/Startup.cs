using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using P02_BillsPaymentSystem.Data.Initialization;

namespace P01_BillsPaymentSystem
{
    public class Startup
    {
	public static void Main()
	{
	    using (var context = new BillsPaymentSystemContext())
	    {
		Initializer.Seed(context);
	    }
	}
    }
}
