using System.IO;
using System.Linq;
using SoftUniDB.Data;

namespace DepartmentsWith5PlusEmployees
{
    public class StartUp
    {
	public static void Main()
	{
	    using (var context = new SoftUniContext())
	    {
		var departments = context.Departments
		    .Where(d => d.Employees.Count > 5)
		    .OrderBy(d => d.Employees.Count)
		    .ThenBy(d => d.Name)
		    .Select(d => new
		    {
			d.Name,
			ManagerName = $"{d.Manager.FirstName} {d.Manager.LastName}",
			Employees = d.Employees
			.OrderBy(e => e.FirstName)
			.ThenBy(e => e.LastName)
			.Select(e => new
			{
			    Name = $"{e.FirstName} {e.LastName}",
			    e.JobTitle
			})
		    })
		    .ToArray();
		using (StreamWriter sw = new StreamWriter(@"..\..\..\DepartmentsWith5PlusEmployees.txt"))
		{
		    foreach (var department in departments)
		    {
			sw.WriteLine($"{department.Name} - {department.ManagerName}");
			foreach (var employee in department.Employees)
			{
			    sw.WriteLine($"{employee.Name} - {employee.JobTitle}");
			}
			sw.WriteLine("----------");
		    }
		}
	    }
	}
    }
}
