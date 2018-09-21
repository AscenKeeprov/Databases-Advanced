namespace P01_HospitalDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Medicaments")]
    public class Medicament : IComparable<Medicament>
    {
	public Medicament()
	{
	    Prescriptions = new HashSet<PatientMedicament>();
	}

	[Key]
	public int MedicamentId { get; set; }

	[Column(TypeName = "NVARCHAR(50)")]
	[MaxLength(50)]
	public string Name { get; set; }

	public virtual ICollection<PatientMedicament> Prescriptions { get; set; }

	public int CompareTo(Medicament other)
	{
	    return Name.CompareTo(other.Name);
	}
    }
}
