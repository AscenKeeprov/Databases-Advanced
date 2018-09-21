using System.IO;
using System.Linq;
using SoftUniDB.Data;

namespace EmployeesFullInformation
{
    public class StartUp
    {
	public static void Main()
	{
	    using (var context = new SoftUniContext())
	    {
		var employees = context.Employees
		    .OrderBy(e => e.EmployeeId)
		    .Select(e => new
		    {
			e.FirstName,
			e.MiddleName,
			e.LastName,
			e.JobTitle,
			e.Salary
		    })
		    .ToArray();
		using (StreamWriter sw = new StreamWriter(@"..\..\..\EmployeesFullInformation.txt"))
		{
		    foreach (var employee in employees)
		    {
			sw.WriteLine($"{employee.FirstName} {employee.LastName} " +
			    $"{employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
		    }
		}
	    }
	}
    }
}
