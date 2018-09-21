namespace MiniORM.App
{
    using System;
    using System.Linq;
    using Data;
    using Data.Entities;

    public class StartUp
    {
	public static void Main()
	{
	    MiniORMDbContext dbContext = new MiniORMDbContext(DbConfiguration.ConnectionString);
	    try
	    {
		/* HINT: Use the debugger to pause after each step
		 * and observe the effects on the database */
		HireEmployee(dbContext);
		UpdateEmployeeData(dbContext);
		FireEmployee(dbContext);
	    }
	    catch (Exception exception)
	    {
		Console.WriteLine(exception.Message);
	    }
	}

	private static void HireEmployee(MiniORMDbContext dbContext)
	{
	    Employee newbie = new Employee
	    {
		FirstName = "Newbie",
		LastName = "Jenkins",
		DepartmentId = dbContext.Departments
		    .Single(d => d.Name == "Quality Assurance").Id,
		IsEmployed = true
	    };
	    dbContext.Employees.Add(newbie);
	    dbContext.SaveChanges();
	    Console.WriteLine($"Employee {newbie.FirstName} {newbie.LastName} hired.");
	}

	private static void UpdateEmployeeData(MiniORMDbContext dbContext)
	{
	    Employee newbie = dbContext.Employees.SingleOrDefault(e
		=> e.FirstName == "Newbie"
		&& e.LastName == "Jenkins");
	    if (newbie is null)
		throw new ArgumentNullException("No such employee on payroll.");
	    newbie.MiddleName = "Rosewood";
	    newbie.DepartmentId = dbContext.Departments
		.Single(d => d.Name == "Research and Development").Id;
	    newbie.Department = dbContext.Departments
		.Single(d => d.Id == newbie.DepartmentId);
	    dbContext.Employees.Update(newbie);
	    dbContext.SaveChanges();
	    Console.WriteLine($"Information for employee {newbie.FirstName} {newbie.LastName} updated.");
	}

	private static void FireEmployee(MiniORMDbContext dbContext)
	{
	    Employee newbie = dbContext.Employees.SingleOrDefault(e
		=> e.FirstName == "Newbie"
		&& e.LastName == "Jenkins"
		&& e.IsEmployed == true);
	    if (newbie is null)
		throw new ArgumentNullException("No such employee on payroll.");
	    dbContext.Employees.Remove(newbie);
	    #region /*Substitute 'Remove' with this, in case InsteadOfDelete logic is implemented:*/
	    //newbie.IsEmployed = false;
	    //dbContext.Employees.Update(newbie);
	    #endregion
	    dbContext.SaveChanges();
	    Console.WriteLine($"Employee {newbie.FirstName} {newbie.LastName} fired.");
	}
    }
}
