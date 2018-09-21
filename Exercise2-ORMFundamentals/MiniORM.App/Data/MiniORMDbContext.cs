namespace MiniORM.App.Data
{
    using Entities;

    public class MiniORMDbContext : DbContext
    {
	public MiniORMDbContext(string connectionString) : base(connectionString) { }

	public DbSet<Department> Departments { get; }
	public DbSet<Employee> Employees { get; }
	public DbSet<Project> Projects { get; }
	public DbSet<EmployeeProject> EmployeesProjects { get; }
    }
}
