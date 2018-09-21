namespace P01_HospitalDatabase.Initializer.Generators
{
    using P01_HospitalDatabase.Data.Models;

    internal static class PrescriptionGenerator
    {
	internal static PatientMedicament GeneratePrescription(int patientId, int medicamentId)
	{
	    PatientMedicament prescription = new PatientMedicament()
	    {
		PatientId = patientId,
		MedicamentId = medicamentId
	    };
	    return prescription;
	}
    }
}
