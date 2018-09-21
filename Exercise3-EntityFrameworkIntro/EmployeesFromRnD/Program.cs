using System.IO;
using System.Linq;
using SoftUniDB.Data;

namespace EmployeesFromRnD
{
    public class StartUp
    {
	public static void Main()
	{
	    using (var context = new SoftUniContext())
	    {
		var employees = context.Employees
		    .OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName)
		    .Where(e => e.Department.Name == "Research and Development")
		    .Select(e => new
		    {
			e.FirstName,
			e.LastName,
			DepartmentName = e.Department.Name,
			e.Salary
		    })
		    .ToArray();
		using (StreamWriter sw = new StreamWriter(@"..\..\..\EmployeesFromRnD.txt"))
		{
		    foreach (var employee in employees)
		    {
			sw.WriteLine($"{employee.FirstName} {employee.LastName} from " +
			    $"{employee.DepartmentName} - ${employee.Salary:F2}");
		    }
		}
	    }
	}
    }
}
