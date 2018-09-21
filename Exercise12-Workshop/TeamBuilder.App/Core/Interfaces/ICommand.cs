using System.Collections.Generic;

namespace TeamBuilder.App.Core.Interfaces
{
    public interface ICommand
    {
	string Execute(IList<string> commandArgs);
    }
}
