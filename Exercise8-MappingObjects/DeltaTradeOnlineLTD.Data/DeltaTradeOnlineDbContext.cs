using DeltaTradeOnlineLTD.Data.EntityConfiguration;
using DeltaTradeOnlineLTD.Models;
using Microsoft.EntityFrameworkCore;

namespace DeltaTradeOnlineLTD.Data
{
    public class DeltaTradeOnlineDbContext : DbContext
    {
	public DeltaTradeOnlineDbContext() { }

	public DeltaTradeOnlineDbContext(DbContextOptions options) : base(options) { }

	public virtual DbSet<Employee> Employees { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
	    if (!optionsBuilder.IsConfigured)
	    {
		optionsBuilder.UseSqlServer(DbConfiguration.ConnectionString);
	    }
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	    modelBuilder.ApplyConfiguration(new EmployeeConfig());
	}
    }
}
