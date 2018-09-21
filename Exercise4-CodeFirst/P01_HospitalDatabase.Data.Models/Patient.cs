namespace P01_HospitalDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Patients")]
    public class Patient : IComparable<Patient>
    {
	public Patient()
	{
	    Visitations = new HashSet<Visitation>();
	    Diagnoses = new HashSet<Diagnose>();
	    Prescriptions = new HashSet<PatientMedicament>();
	}

	[Key]
	public int PatientId { get; set; }

	[Column(TypeName = "NVARCHAR(50)")]
	[MaxLength(50)]
	public string FirstName { get; set; }

	[Column(TypeName = "NVARCHAR(50)")]
	[MaxLength(50)]
	public string LastName { get; set; }

	[Column(TypeName = "NVARCHAR(250)")]
	[MaxLength(250)]
	public string Address { get; set; }

	[Column(TypeName = "VARCHAR(80)")]
	[MaxLength(80)]
	public string Email { get; set; }

	public bool HasInsurance { get; set; }

	public virtual ICollection<Visitation> Visitations { get; set; }
	public virtual ICollection<Diagnose> Diagnoses { get; set; }
	public virtual ICollection<PatientMedicament> Prescriptions { get; set; }

	public override string ToString()
	{
	    return $"{FirstName} {LastName} - {Address}";
	}

	public int CompareTo(Patient other)
	{
	    return ToString().CompareTo(other.ToString());
	}
    }
}
