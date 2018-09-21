using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoftUniDB.Data;

namespace EmployeesNamesStartingWithSa
{
    public class StartUp
    {
	public static void Main()
	{
	    using (var context = new SoftUniContext())
	    {
		var employees = context.Employees
		    .Where(e => EF.Functions.Like(e.FirstName, "Sa%"))
		    .OrderBy(e => e.FirstName)
		    .ThenBy(e => e.LastName)
		    .Select(e => new
		    {
			e.FirstName,
			e.LastName,
			e.JobTitle,
			e.Salary
		    })
		    .ToArray();
		using (StreamWriter sw = new StreamWriter(@"..\..\..\EmployeesNamesStartingWithSa.txt"))
		{
		    foreach (var employee in employees)
		    {
			sw.WriteLine($"{employee.FirstName} {employee.LastName}" +
			    $" - {employee.JobTitle} - (${employee.Salary:F2})");
		    }
		}
	    }
	}
    }
}
