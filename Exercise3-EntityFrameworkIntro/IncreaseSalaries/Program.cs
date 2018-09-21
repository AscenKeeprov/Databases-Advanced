using System.IO;
using System.Linq;
using SoftUniDB.Data;
using SoftUniDB.Data.Models;

namespace IncreaseSalaries
{
    public class StartUp
    {
	public static void Main()
	{
	    string[] departmentNames = new string[]
		{
		    "Engineering",
		    "Information Services",
		    "Marketing",
		    "Tool Design"
		};
	    using (var context = new SoftUniContext())
	    {
		context.Employees
		    .Where(e => departmentNames.Contains(e.Department.Name))
		    .ToList().ForEach(e => e.Salary *= 1.12M);
		context.SaveChanges();
		var happyEmployees = context.Employees
		    .Where(e => departmentNames.Contains(e.Department.Name))
		    .OrderBy(e => e.FirstName)
		    .ThenBy(e => e.LastName)
		    .Select(e => new Employee
		    {
			FirstName = e.FirstName,
			LastName = e.LastName,
			Salary = e.Salary
		    })
		    .ToArray();
		using (StreamWriter sw = new StreamWriter(@"..\..\..\IncreaseSalaries.txt"))
		{
		    foreach (var employee in happyEmployees)
		    {
			sw.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F2})");
		    }
		}
	    }
	}
    }
}
