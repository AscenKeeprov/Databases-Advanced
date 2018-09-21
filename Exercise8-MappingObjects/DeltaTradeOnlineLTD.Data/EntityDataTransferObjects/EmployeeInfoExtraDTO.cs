namespace DeltaTradeOnlineLTD.Data.EntityDataTransferObjects
{
    public class EmployeeInfoExtraDTO : EmployeeInfoBasicDTO
    {
	public int Id { get; set; }

	public override string ToString()
	{
	    return $"ID: {Id} - {base.ToString()}";
	}
    }
}
