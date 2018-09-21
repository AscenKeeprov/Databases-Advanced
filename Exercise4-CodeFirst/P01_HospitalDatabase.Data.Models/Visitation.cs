namespace P01_HospitalDatabase.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Visitations")]
    public class Visitation : IComparable<Visitation>
    {
	[Key]
	public int VisitationId { get; set; }

	[Column(TypeName = "DATETIME2")]
	public DateTime Date { get; set; }

	[Column(TypeName = "NVARCHAR(250)")]
	[MaxLength(250)]
	public string Comments { get; set; }

	public int PatientId { get; set; }
	public virtual Patient Patient { get; set; }

	public int? DoctorId { get; set; }
	public virtual Doctor Doctor { get; set; }

	public int CompareTo(Visitation other)
	{
	    return ToString().CompareTo(other.ToString());
	}

	public override string ToString()
	{
	    return $"{PatientId} - {Date}";
	}
    }
}
