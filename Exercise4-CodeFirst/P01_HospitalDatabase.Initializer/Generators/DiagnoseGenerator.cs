namespace P01_HospitalDatabase.Initializer.Generators
{
    using System;
    using P01_HospitalDatabase.Data.Models;

    internal static class DiagnoseGenerator
    {
	private static Random rng = new Random();

	private static string[] diagnoseNames = new string[]
	{
	    "Limp Scurvy",
	    "Fading Infection",
	    "Cow Feet",
	    "Incurable Ebola",
	    "Snake Blight",
	    "Spider Asthma",
	    "Sinister Body",
	    "Spine Diptheria",
	    "Pygmy Decay",
	    "King's Arthritis",
	    "Desert Rash",
	    "Deteriorating Salmonella",
	    "Shadow Anthrax",
	    "Hiccup Meningitis",
	    "Fading Depression",
	    "Lion Infertility",
	    "Wolf Delirium",
	    "Humming Measles",
	    "Incurable Stomach",
	    "Grave Heart"
	};

	internal static Diagnose GenerateDiagnose(Patient patient)
	{
	    string diagnoseName = diagnoseNames[rng.Next(diagnoseNames.Length)];
	    Diagnose diagnose = new Diagnose()
	    {
		Name = diagnoseName,
		Patient = patient
	    };
	    return diagnose;
	}
    }
}
