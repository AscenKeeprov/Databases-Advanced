namespace P01_HospitalDatabase.Initializer.Generators
{
    using System;
    using P01_HospitalDatabase.Data.Models;

    internal static class VisitationGenerator
    {
	internal static Visitation GenerateVisitation(Patient patient)
	{
	    DateTime visitationDate = DateGenerator.GenerateDate();
	    Visitation visitation = new Visitation()
	    {
		Date = visitationDate,
		Patient = patient
	    };
	    return visitation;
	}
    }
}
