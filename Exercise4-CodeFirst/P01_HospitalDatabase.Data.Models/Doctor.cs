namespace P01_HospitalDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Doctors")]
    public class Doctor : IComparable<Doctor>
    {
	public Doctor()
	{
	    Visitations = new HashSet<Visitation>();
	}

	[Key]
	public int DoctorId { get; set; }

	[Column(TypeName = "NVARCHAR(100)")]
	[MaxLength(100)]
	public string Name { get; set; }

	[Column(TypeName = "NVARCHAR(100)")]
	[MaxLength(100)]
	public string Specialty { get; set; }

	public virtual ICollection<Visitation> Visitations { get; set; }

	public override string ToString()
	{
	    return $"{Name} - {Specialty}";
	}

	public int CompareTo(Doctor other)
	{
	    return ToString().CompareTo(other.ToString());
	}
    }
}
