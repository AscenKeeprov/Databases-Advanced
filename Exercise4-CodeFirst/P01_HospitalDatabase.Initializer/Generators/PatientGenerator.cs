namespace P01_HospitalDatabase.Initializer.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using P01_HospitalDatabase.Data;
    using P01_HospitalDatabase.Data.Models;

    internal class PatientGenerator
    {
	private static Random rng = new Random();

	public static Patient GeneratePatient(HospitalContext context)
	{
	    string firstName = NameGenerator.GenerateFirstName();
	    string lastName = NameGenerator.GenerateLastName();
	    string address = AddressGenerator.GenerateAddress();
	    string email = EmailGenerator.GenerateEmail(firstName + lastName);
	    Patient patient = new Patient()
	    {
		FirstName = firstName,
		LastName = lastName,
		Address = address,
		Email = email
	    };
	    patient.Visitations = SeedVisitations(patient);
	    patient.Diagnoses = SeedDiagnoses(patient);
	    return patient;
	}

	private static ICollection<Visitation> SeedVisitations(Patient patient)
	{
	    int visitationCount = rng.Next(1, 5);
	    var visitations = new List<Visitation>();
	    for (int vis = 1; vis <= visitationCount; vis++)
	    {
		Visitation visitation = VisitationGenerator.GenerateVisitation(patient);
		if (!visitations.Any(v => v.CompareTo(visitation) == 0))
		    visitations.Add(visitation);
	    }
	    return visitations;
	}

	private static ICollection<Diagnose> SeedDiagnoses(Patient patient)
	{
	    int diagnoseCount = rng.Next(1, 4);
	    var diagnoses = new List<Diagnose>();
	    for (int diag = 1; diag <= diagnoseCount; diag++)
	    {
		Diagnose diagnose = DiagnoseGenerator.GenerateDiagnose(patient);
		if (!diagnoses.Any(d => d.CompareTo(diagnose) == 0))
		    diagnoses.Add(diagnose);
	    }
	    return diagnoses;
	}
    }
}
