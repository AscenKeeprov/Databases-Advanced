using System.Globalization;
using System.IO;
using System.Linq;
using SoftUniDB.Data;

namespace EmployeesAndProjects
{
    public class StartUp
    {
	public static void Main()
	{
	    string dateFormat = "M/d/yyyy h:mm:ss tt";
	    CultureInfo culture = CultureInfo.InvariantCulture;
	    using (var context = new SoftUniContext())
	    {
		var employees = context.Employees
		    .Where(e => e.EmployeesProjects.Any(
			p => p.Project.StartDate.Year >= 2001
			&& p.Project.StartDate.Year <= 2003))
		    .Select(e => new
		    {
			e.FirstName,
			e.LastName,
			ManagerFirstName = e.Manager.FirstName,
			ManagerLasttName = e.Manager.LastName,
			Projects2001To2003 = e.EmployeesProjects
			.Select(p => new
			{
			    p.Project.Name,
			    p.Project.StartDate,
			    p.Project.EndDate
			})
		    })
		    .Take(30).ToArray();
		using (StreamWriter sw = new StreamWriter(@"..\..\..\EmployeesAndProjects.txt"))
		{
		    foreach (var employee in employees)
		    {
			sw.WriteLine($"{employee.FirstName} {employee.LastName}" +
			    $" - Manager: {employee.ManagerFirstName} {employee.ManagerLasttName}");
			foreach (var project in employee.Projects2001To2003)
			{
			    sw.WriteLine($"--{project.Name}" +
				$" - {project.StartDate.ToString(dateFormat, culture)}" +
				$" - {project.EndDate?.ToString(dateFormat, culture) ?? "not finished"}");
			}
		    }
		}
	    }
	}
    }
}
