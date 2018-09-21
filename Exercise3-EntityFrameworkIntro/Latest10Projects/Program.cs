using System.Globalization;
using System.IO;
using System.Linq;
using SoftUniDB.Data;

namespace Latest10Projects
{
    public class StartUp
    {
	public static void Main()
	{
	    string dateFormat = "M/d/yyyy h:mm:ss tt";
	    CultureInfo culture = CultureInfo.InvariantCulture;
	    using (var context = new SoftUniContext())
	    {
		var projects = context.Projects
		    .OrderByDescending(p => p.StartDate)
		    .Select(p => new
		    {
			p.Name,
			p.Description,
			p.StartDate
		    })
		    .Take(10).ToArray();
		using (StreamWriter sw = new StreamWriter(@"..\..\..\Latest10Projects.txt"))
		{
		    foreach (var project in projects.OrderBy(p => p.Name))
		    {
			sw.WriteLine(project.Name);
			sw.WriteLine(project.Description);
			sw.WriteLine(project.StartDate.ToString(dateFormat, culture));
		    }
		}
	    }
	}
    }
}
