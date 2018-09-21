namespace P01_HospitalDatabase.Initializer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data;
    using P01_HospitalDatabase.Data.Models;
    using P01_HospitalDatabase.Initializer.Generators;

    public static class DatabaseInitializer
    {
	private static Random rng = new Random();
	private const int SeedCountLowerBound = 10;
	private const int SeedCountUpperBound = SeedCountLowerBound * 2 + 10;
	private static int SeedCountOffset => Math.Abs(SeedCountUpperBound - SeedCountLowerBound) / 2;

	public static void ReseedDatabase(HospitalContext context)
	{
	    context.Database.EnsureDeleted();
	    SeedDatabase(context);
	}

	public static void SeedDatabase(HospitalContext context)
	{
	    context.Database.Migrate();
	    int seedCount = rng.Next(SeedCountLowerBound, SeedCountUpperBound);
	    SeedMedicaments(context, Math.Abs(seedCount + SeedCountOffset));
	    SeedPatients(context, Math.Abs(seedCount - SeedCountOffset));
	    SeedPrescriptions(context, Math.Abs(seedCount));
	}

	private static void SeedMedicaments(HospitalContext context, int medicamentSeeds)
	{
	    var medicaments = context.Medicaments.ToList();
	    for (int ms = 1; ms <= medicamentSeeds; ms++)
	    {
		Medicament medicament = MedicamentGenerator.GenerateMedicament();
		if (!medicaments.Any(m => m.CompareTo(medicament) == 0))
		    medicaments.Add(medicament);
	    }
	    context.Medicaments.AddRange(medicaments);
	    context.SaveChanges();
	}

	private static void SeedPatients(HospitalContext context, int patientSeeds)
	{
	    var patients = context.Patients.ToList();
	    for (int ps = 1; ps <= patientSeeds; ps++)
	    {
		Patient patient = PatientGenerator.GeneratePatient(context);
		if (!patients.Any(p => p.CompareTo(patient) == 0))
		    patients.Add(patient);
	    }
	    context.Patients.AddRange(patients);
	    context.SaveChanges();
	}

	private static void SeedPrescriptions(HospitalContext context, int prescriptionSeeds)
	{
	    int[] patientIds = context.Patients.Select(p => p.PatientId).ToArray();
	    int[] medicamentIds = context.Medicaments.Select(m => m.MedicamentId).ToArray();
	    if (patientIds.Length > 0 && medicamentIds.Length > 0)
	    {
		var prescriptions = context.PatientsMedicaments.ToList();
		for (int prs = 1; prs <= prescriptionSeeds; prs++)
		{
		    int patientId = patientIds[rng.Next(patientIds.Length)];
		    int medicamentId = medicamentIds[rng.Next(medicamentIds.Length)];
		    PatientMedicament prescription = PrescriptionGenerator
			.GeneratePrescription(patientId, medicamentId);
		    if (!prescriptions.Any(pr => pr.CompareTo(prescription) == 0))
			prescriptions.Add(prescription);
		}
		context.PatientsMedicaments.AddRange(prescriptions);
		context.SaveChanges();
	    }
	}
    }
}
