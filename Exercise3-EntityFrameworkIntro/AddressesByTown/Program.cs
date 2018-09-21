using System.IO;
using System.Linq;
using SoftUniDB.Data;

namespace AddressesByTown
{
    public class StartUp
    {
	public static void Main()
	{
	    using (var context = new SoftUniContext())
	    {
		var addresses = context.Addresses
		    .Select(a => new
		    {
			a.AddressText,
			TownName = a.Town.Name,
			EmployeesCount = a.Employees.Count
		    })
		    .OrderByDescending(a => a.EmployeesCount)
		    .ThenBy(a => a.TownName)
		    .ThenBy(a => a.AddressText)
		    .Take(10).ToArray();
		using (StreamWriter sw = new StreamWriter(@"..\..\..\AddressesByTown.txt"))
		{
		    foreach (var address in addresses)
		    {
			sw.WriteLine($"{address.AddressText}, {address.TownName}" +
			    $" - {address.EmployeesCount} employees");
		    }
		}
	    }
	}
    }
}
