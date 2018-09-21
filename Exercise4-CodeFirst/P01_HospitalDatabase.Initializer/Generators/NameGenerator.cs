namespace P01_HospitalDatabase.Initializer.Generators
{
    using System;

    internal static class NameGenerator
    {
	private static Random rng = new Random();

	private static string[] firstNames = new string[]
	{
	    "Petur",
	    "Ivan",
	    "Georgi",
	    "Alexander",
	    "Stefan",
	    "Vladimir",
	    "Svetoslav",
	    "Kaloyan",
	    "Mihail",
	    "Stamat"
	};

	private static string[] lastNames = new string[]
	{
	    "Ivanov",
	    "Georgiev",
	    "Stefanov",
	    "Alexandrov",
	    "Petrov",
	    "Stamatkov"
	};

	internal static string GenerateFirstName()
	{
	    string firstName = firstNames[rng.Next(firstNames.Length)];
	    return firstName;
	}

	internal static string GenerateLastName()
	{
	    string lastName = lastNames[rng.Next(lastNames.Length)];
	    return lastName;
	}
    }
}
