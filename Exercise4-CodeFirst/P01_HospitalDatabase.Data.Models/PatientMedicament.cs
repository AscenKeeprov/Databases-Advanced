namespace P01_HospitalDatabase.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PatientsMedicaments")]
    public class PatientMedicament : IComparable<PatientMedicament>
    {
	public int PatientId { get; set; }
	public Patient Patient { get; set; }

	public int MedicamentId { get; set; }
	public Medicament Medicament { get; set; }

	public override string ToString()
	{
	    return $"{PatientId} - {MedicamentId}";
	}

	public int CompareTo(PatientMedicament other)
	{
	    return ToString().CompareTo(other.ToString());
	}
    }
}
