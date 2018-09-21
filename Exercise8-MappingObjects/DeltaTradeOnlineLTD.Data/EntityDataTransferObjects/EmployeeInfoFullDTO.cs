using System.Text;

namespace DeltaTradeOnlineLTD.Data.EntityDataTransferObjects
{
    public class EmployeeInfoFullDTO : EmployeeInfoExtraDTO
    {
	public string BirthDate { get; set; }
	public string Address { get; set; }

	public override string ToString()
	{
	    StringBuilder employeeInfo = new StringBuilder();
	    employeeInfo.AppendLine(base.ToString());
	    employeeInfo.AppendLine($"Birthday: {BirthDate}");
	    employeeInfo.AppendLine($"Address: {(Address ?? "unknown")}");
	    return employeeInfo.ToString().TrimEnd();
	}
    }
}
