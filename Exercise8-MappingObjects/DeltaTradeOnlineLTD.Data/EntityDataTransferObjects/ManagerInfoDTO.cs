using System.Collections.Generic;
using System.Text;

namespace DeltaTradeOnlineLTD.Data.EntityDataTransferObjects
{
    public class ManagerInfoDTO
    {
	public string Name { get; set; }
	public int SubordinatesCount { get; set; }
	public ICollection<EmployeeInfoBasicDTO> Subordinates { get; set; }

	public override string ToString()
	{
	    StringBuilder managerInfo = new StringBuilder();
	    managerInfo.AppendLine($"{Name} | Employees: {SubordinatesCount}");
	    foreach (var employeeInfo in Subordinates)
	    {
		managerInfo.AppendLine($"    - {employeeInfo.ToString()}");
	    }
	    return managerInfo.ToString().TrimEnd();
	}
    }
}
