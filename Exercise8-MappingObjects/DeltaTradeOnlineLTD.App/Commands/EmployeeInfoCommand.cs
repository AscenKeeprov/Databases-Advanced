using System;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Data.EntityDataTransferObjects;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App.Commands
{
    public class EmployeeInfoCommand : Command
    {
	protected override int MinRequiredParameters => 1;
	protected override int MaxAllowedParameters => 1;

	private readonly IEmployeeController employeeController;

	public EmployeeInfoCommand(IEmployeeController employeeController, params string[] parameters)
	    : base(parameters)
	{
	    this.employeeController = employeeController;
	}

	public override void Execute()
	{
	    int employeeId = int.Parse(Parameters[0]);
	    EmployeeInfoExtraDTO dto = employeeController.GetInfo(employeeId);
	    Console.WriteLine(dto.ToString());
	}
    }
}
