namespace DeltaTradeOnlineLTD.Data.EntityDataTransferObjects
{
    public class EmployeeInfoBasicDTO
    {
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public decimal Salary { get; set; }

	public override string ToString()
	{
	    return $"{FirstName} {LastName} - ${Salary:F2}";
	}
    }
}
