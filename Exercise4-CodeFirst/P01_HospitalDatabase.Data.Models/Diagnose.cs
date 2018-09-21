namespace P01_HospitalDatabase.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Diagnoses")]
    public class Diagnose : IComparable<Diagnose>
    {
	[Key]
	public int DiagnoseId { get; set; }

	[Column(TypeName = "NVARCHAR(50)")]
	[MaxLength(50)]
	public string Name { get; set; }

	[Column(TypeName = "NVARCHAR(250)")]
	[MaxLength(250)]
	public string Comments { get; set; }

	public int PatientId { get; set; }
	public virtual Patient Patient { get; set; }

	public int CompareTo(Diagnose other)
	{
	    return Name.CompareTo(other.Name);
	}

	public override string ToString()
	{
	    return $"{PatientId} - {Name}";
	}
    }
}
