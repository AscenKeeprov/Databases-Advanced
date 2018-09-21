using System;
using System.Linq;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App.Commands
{
    public class SetAddressCommand : Command
    {
	protected override int MinRequiredParameters => 2;
	protected override int MaxAllowedParameters => SByte.MaxValue;

	private readonly IEmployeeController employeeController;

	public SetAddressCommand(IEmployeeController employeeController, params string[] parameters)
	    : base(parameters)
	{
	    this.employeeController = employeeController;
	}

	public override void Execute()
	{
	    int employeeId = int.Parse(Parameters[0]);
	    string address = String.Join(" ", Parameters.Skip(1));
	    employeeController.SetAddress(employeeId, address);
	    Console.WriteLine($"Address for employee #{employeeId} updated.");
	}
    }
}
