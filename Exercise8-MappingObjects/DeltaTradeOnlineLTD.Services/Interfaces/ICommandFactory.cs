using System.Collections.Generic;
using DeltaTradeOnlineLTD.Models.Interfaces;

namespace DeltaTradeOnlineLTD.Services.Interfaces
{
    public interface ICommandFactory
    {
	ICommand CreateCommand(ICollection<string> commandInput);
    }
}
