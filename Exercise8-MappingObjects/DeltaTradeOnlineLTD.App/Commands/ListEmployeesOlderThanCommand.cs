using System;
using System.Collections.Generic;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Data.EntityDataTransferObjects;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App.Commands
{
    public class ListEmployeesOlderThanCommand : Command
    {
	protected override int MinRequiredParameters => 1;
	protected override int MaxAllowedParameters => 1;

	private readonly IEmployeeController employeeController;

	public ListEmployeesOlderThanCommand(IEmployeeController employeeController, params string[] parameters)
	    : base(parameters)
	{
	    this.employeeController = employeeController;
	}

	public override void Execute()
	{
	    int age = int.Parse(Parameters[0]);
	    ICollection<EmployeeSalManDTO> employeeDTOs = employeeController.ListEmployeesOlderThan(age);
	    foreach (var employeeDTO in employeeDTOs)
	    {
		Console.WriteLine(employeeDTO.ToString());
	    }
	}
    }
}
