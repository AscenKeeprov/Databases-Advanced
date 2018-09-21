using System;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App.Commands
{
    public class GetAddressCommand : Command
    {
	private const string AddressMessage = "Employee #{0}'s address is {1}.";

	protected override int MinRequiredParameters => 1;
	protected override int MaxAllowedParameters => 1;

	private readonly IEmployeeController employeeController;

	public GetAddressCommand(IEmployeeController employeeController, params string[] parameters)
	    : base(parameters)
	{
	    this.employeeController = employeeController;
	}

	public override void Execute()
	{
	    int employeeId = int.Parse(Parameters[0]);
	    string address = employeeController.GetAddress(employeeId);
	    Console.WriteLine(String.Format(AddressMessage, employeeId, address ?? "unknown"));
	}
    }
}
