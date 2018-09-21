using System;
using System.Collections.Generic;
using DeltaTradeOnlineLTD.Models.Interfaces;

namespace DeltaTradeOnlineLTD.Models
{
    public class Employee : INameable, ILocatable, ISalaried
    {
	public Employee()
	{
	    Subordinates = new HashSet<Employee>();
	}

	public int EmployeeId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public decimal Salary { get; set; }
	public DateTime? Birthday { get; set; }
	public string Address { get; set; }

	public int? ManagerId { get; set; }
	public virtual Employee Manager { get; set; }

	public ICollection<Employee> Subordinates { get; set; }
    }
}
