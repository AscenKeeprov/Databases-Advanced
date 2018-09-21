using System;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Data.EntityDataTransferObjects;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App.Commands
{
    public class RemoveEmployeeCommand : Command
    {
	protected override int MinRequiredParameters => 1;
	protected override int MaxAllowedParameters => 1;

	private readonly IEmployeeController employeeController;

	public RemoveEmployeeCommand(IEmployeeController employeeController, params string[] parameters)
	    : base(parameters)
	{
	    this.employeeController = employeeController;
	}

	public override void Execute()
	{
	    int employeeId = int.Parse(Parameters[0]);
	    EmployeeInfoBasicDTO employeeDTO = employeeController.Fire(employeeId);
	    Console.WriteLine($"Employee {employeeDTO.FirstName} {employeeDTO.LastName} fired.");
	}
    }
}
