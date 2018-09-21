using System;
using DeltaTradeOnlineLTD.Models.Interfaces;

namespace DeltaTradeOnlineLTD.Models
{
    public abstract class Command : ICommand
    {
	private const string InvalidNumberOfParametersException = "{0} does not work with {1} parameter{2}!";

	protected virtual int MinRequiredParameters => 0;
	protected virtual int MaxAllowedParameters => 3;
	private string[] parameters;

	public string[] Parameters
	{
	    get { return parameters; }
	    private set
	    {
		if (value.Length < MinRequiredParameters || value.Length > MaxAllowedParameters)
		{
		    throw new ArgumentException(String.Format
		    (
			InvalidNumberOfParametersException,
			GetType().Name, value.Length,
			(value.Length == 1 ? String.Empty : "s")));
		}
		else parameters = value;
	    }
	}

	public Command(params string[] parameters)
	{
	    Parameters = parameters;
	}

	public abstract void Execute();
    }
}
