using System;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Data.EntityDataTransferObjects;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App.Commands
{
    public class SetBirthdayCommand : Command
    {
	protected override int MinRequiredParameters => 2;
	protected override int MaxAllowedParameters => 2;

	private readonly IEmployeeController employeeController;

	public SetBirthdayCommand(IEmployeeController employeeController, params string[] parameters)
	    : base(parameters)
	{
	    this.employeeController = employeeController;
	}

	public override void Execute()
	{
	    int employeeId = int.Parse(Parameters[0]);
	    string birthDate = Parameters[1];
	    EmployeeBirthdayDTO employeeDTO = new EmployeeBirthdayDTO()
	    {
		Id = employeeId,
		BirthDate = birthDate
	    };
	    employeeController.SetBirthday(employeeDTO);
	    Console.WriteLine($"Birthday for employee #{employeeId} updated.");
	}
    }
}
