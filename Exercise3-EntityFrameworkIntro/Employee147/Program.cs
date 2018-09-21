using System.IO;
using System.Linq;
using SoftUniDB.Data;
using SoftUniDB.Data.Models;

namespace Employee147
{
    public class StartUp
    {
	public static void Main()
	{
	    using (var context = new SoftUniContext())
	    {
		Employee employee147 = context.Employees
		    .SingleOrDefault(e => e.EmployeeId == 147);
		var projects147 = context.Projects
		    .Where(p => p.EmployeesProjects.Any(e => e.EmployeeId == 147))
		    .ToArray();
		using (StreamWriter sw = new StreamWriter(@"..\..\..\Employee147.txt"))
		{
		    sw.WriteLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");
		    foreach (var project in projects147.OrderBy(p => p.Name))
		    {
			sw.WriteLine(project.Name);
		    }
		}
	    }
	}
    }
}
