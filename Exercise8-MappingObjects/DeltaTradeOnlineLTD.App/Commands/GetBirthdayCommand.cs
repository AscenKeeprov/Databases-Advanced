using System;
using System.Globalization;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Data.EntityDataTransferObjects;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App.Commands
{
    public class GetBirthdayCommand : Command
    {
	private const string BirthdayKnown = "Employee #{0} has birthday on {1} {2}.";
	private const string BirthdayUnknown = "Employee #{0}'s birthday is {1}.";

	protected override int MinRequiredParameters => 1;
	protected override int MaxAllowedParameters => 1;

	private readonly IEmployeeController employeeController;

	public GetBirthdayCommand(IEmployeeController employeeController, params string[] parameters)
	    : base(parameters)
	{
	    this.employeeController = employeeController;
	}

	public override void Execute()
	{
	    int employeeId = int.Parse(Parameters[0]);
	    EmployeeBirthdayDTO dto = employeeController.GetBirthday(employeeId);
	    if (DateTime.TryParse(dto.BirthDate, out DateTime birthday))
		Console.WriteLine(String.Format(BirthdayKnown, dto.Id, birthday.Day, birthday.ToString("MMMM", CultureInfo.InvariantCulture)));
	    else Console.WriteLine(String.Format(BirthdayUnknown, dto.Id, dto.BirthDate));
	}
    }
}
