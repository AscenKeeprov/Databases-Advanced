using System;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Data.EntityDataTransferObjects;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App.Commands
{
    public class SetManagerCommand : Command
    {
	protected override int MinRequiredParameters => 2;
	protected override int MaxAllowedParameters => 2;

	private readonly IEmployeeController employeeController;

	public SetManagerCommand(IEmployeeController employeeController, params string[] parameters)
	    : base(parameters)
	{
	    this.employeeController = employeeController;
	}

	public override void Execute()
	{
	    int employeeId = int.Parse(Parameters[0]);
	    int managerId = int.Parse(Parameters[1]);
	    EmployeeManagerDTO dto = new EmployeeManagerDTO()
	    {
		EmployeeId = employeeId,
		ManagerId = managerId
	    };
	    dto = employeeController.SetManager(dto);
	    Console.WriteLine($"{dto.EmployeeName} is now part of {dto.ManagerName}'s team.");
	}
    }
}
