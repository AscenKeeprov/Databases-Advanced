using System.IO;
using System.Linq;
using SoftUniDB.Data;
using SoftUniDB.Data.Models;

namespace DeleteProjectByID
{
    public class StartUp
    {
	public static void Main()
	{
	    using (var context = new SoftUniContext())
	    {
		Project project2 = context.Projects.Find(2);
		context.EmployeesProjects.RemoveRange(
		    context.EmployeesProjects
		    .Where(record => record.Project.Equals(project2))
		    .ToArray());
		#region /* Alternative way of deleting records from the mapping table: */
		//context.EmployeesProjects
		//    .Where(record => record.Project.Equals(project2))
		//    .ToList().ForEach(record => context.Remove(record));
		#endregion
		context.Projects.Remove(project2);
		context.SaveChanges();
		var remainingProjects = context.Projects
		    .Select(p => new
		    {
			p.Name
		    })
		    .Take(10).ToArray();
		using (StreamWriter sw = new StreamWriter(@"..\..\..\DeleteProjectByID.txt"))
		{
		    foreach (var project in remainingProjects)
		    {
			sw.WriteLine(project.Name);
		    }
		}
	    }
	}
    }
}
