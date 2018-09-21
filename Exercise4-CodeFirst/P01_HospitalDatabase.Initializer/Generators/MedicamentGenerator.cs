namespace P01_HospitalDatabase.Initializer.Generators
{
    using System;
    using P01_HospitalDatabase.Data.Models;

    internal static class MedicamentGenerator
    {
	private static Random rng = new Random();

	private static string[] medicamentNames = new string[]
	{
	    "Biseptol",
	    "Ciclobenzaprina",
	    "Curam",
	    "Diclofenaco",
	    "Disflatyl",
	    "Duvadilan",
	    "Efedrin",
	    "Flanax",
	    "Fluimucil",
	    "Navidoxine",
	    "Nistatin",
	    "Olfen",
	    "Pentrexyl",
	    "Primolut Nor",
	    "Primperan",
	    "Propoven",
	    "Reglin",
	    "Terramicina Oftalmica",
	    "Ultran",
	    "Viartril-S"
	};

	internal static Medicament GenerateMedicament()
	{
	    string medicamentName = medicamentNames[rng.Next(medicamentNames.Length)];
	    Medicament medicament = new Medicament() { Name = medicamentName };
	    return medicament;
	}
    }
}
