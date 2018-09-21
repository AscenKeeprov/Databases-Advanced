namespace DeltaTradeOnlineLTD.Data.EntityDataTransferObjects
{
    public class EmployeeSalManDTO
    {
	public string EmployeeFullName { get; set; }
	public decimal EmployeeSalary { get; set; }
	public string ManagerLastName { get; set; }

	public override string ToString()
	{
	    return $"{EmployeeFullName} - ${EmployeeSalary:F2} - Manager: {(ManagerLastName ?? "[no manager]")}";
	}
    }
}
