using System.IO;
using System.Linq;
using SoftUniDB.Data;

namespace EmployeesWithSalaryOver50K
{
    public class StartUp
    {
	public static void Main()
	{
	    using (var context = new SoftUniContext())
	    {
		var employees = context.Employees
		    .OrderBy(e => e.FirstName)
		    .Where(e => e.Salary > 50000)
		    .Select(e => e.FirstName)
		    .ToArray();
		using (StreamWriter sw = new StreamWriter(@"..\..\..\EmployeesWithSalaryOver50K.txt"))
		{
		    foreach (var employee in employees)
		    {
			sw.WriteLine(employee);
		    }
		}
	    }
	}
    }
}
