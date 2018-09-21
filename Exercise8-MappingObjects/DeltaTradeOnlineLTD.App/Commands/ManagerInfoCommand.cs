using System;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Data.EntityDataTransferObjects;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App.Commands
{
    public class ManagerInfoCommand : Command
    {
	private const string NotManager = "Employee #{0} is not a manager.";

	protected override int MinRequiredParameters => 1;
	protected override int MaxAllowedParameters => 1;

	private readonly IEmployeeController employeeController;

	public ManagerInfoCommand(IEmployeeController employeeController, params string[] parameters)
	    : base(parameters)
	{
	    this.employeeController = employeeController;
	}

	public override void Execute()
	{
	    int managerId = int.Parse(Parameters[0]);
	    ManagerInfoDTO managerDTO = employeeController.GetManagerInfo(managerId);
	    if (managerDTO.SubordinatesCount == 0)
		Console.WriteLine(String.Format(NotManager, managerId));
	    else Console.WriteLine(managerDTO.ToString());
	}
    }
}
