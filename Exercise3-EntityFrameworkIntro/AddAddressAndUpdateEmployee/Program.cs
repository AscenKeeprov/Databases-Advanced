using System.IO;
using System.Linq;
using SoftUniDB.Data;
using SoftUniDB.Data.Models;

namespace AddAddressAndUpdateEmployee
{
    public class StartUp
    {
	public static void Main()
	{
	    using (var context = new SoftUniContext())
	    {
		Address address = new Address()
		{
		    AddressText = "Vitoshka 15",
		    TownId = 4
		};
		context.Addresses.Add(address);
		Employee employee = context.Employees
		    .FirstOrDefault(e => e.LastName == "Nakov");
		employee.Address = address;
		context.SaveChanges();
		var employeeAddresses = context.Employees
		    .OrderByDescending(e => e.AddressId)
		    .Select(e => e.Address.AddressText)
		    .Take(10).ToArray();
		using (StreamWriter sw = new StreamWriter(@"..\..\..\AddAddressAndUpdateEmployee.txt"))
		{
		    foreach (var employeeAddress in employeeAddresses)
		    {
			sw.WriteLine(employeeAddress);
		    }
		}
	    }
	}
    }
}
