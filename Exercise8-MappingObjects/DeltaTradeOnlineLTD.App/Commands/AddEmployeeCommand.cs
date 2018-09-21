using System;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Data.EntityDataTransferObjects;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App.Commands
{
    public class AddEmployeeCommand : Command
    {
	protected override int MinRequiredParameters => 3;
	protected override int MaxAllowedParameters => 3;

	private readonly IEmployeeController employeeController;

	public AddEmployeeCommand(IEmployeeController employeeController, params string[] parameters)
	    : base(parameters)
	{
	    this.employeeController = employeeController;
	}

	public override void Execute()
	{
	    string firstName = Parameters[0];
	    string lastName = Parameters[1];
	    decimal salary = decimal.Parse(Parameters[2]);
	    EmployeeInfoBasicDTO employeeDTO = new EmployeeInfoBasicDTO()
	    {
		FirstName = firstName,
		LastName = lastName,
		Salary = salary
	    };
	    employeeController.Hire(employeeDTO);
	    Console.WriteLine($"Employee {employeeDTO.FirstName} {employeeDTO.LastName} hired.");
	}
    }
}
