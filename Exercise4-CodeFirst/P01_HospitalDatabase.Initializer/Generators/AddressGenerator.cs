namespace P01_HospitalDatabase.Initializer.Generators
{
    using System;

    internal static class AddressGenerator
    {
	private static Random rng = new Random();

	private static string[] townNames = new string[]
	{
	    "Kinecardine",
	    "Whitebridge ",
	    "Nuxvar",
	    "Laencaster",
	    "Lowestoft",
	    "Brickelwhyte ",
	    "Skegness",
	    "Tenby",
	    "Dalmellington",
	    "Ormkirk"
	};

	private static string[] streetNames = new string[]
	{
	    "Somerset Drive",
	    "Prospect Street",
	    "Highland Avenue",
	    "Windsor Court",
	    "College Avenue",
	    "Green Street",
	    "Colonial Avenue",
	    "Elm Avenue",
	    "Durham Road",
	    "Rose Street",
	    "6th Street North",
	    "Brandywine Drive",
	    "Madison Avenue",
	    "Route 10",
	    "Main Street East"
	};

	internal static string GenerateAddress()
	{
	    string townName = townNames[rng.Next(townNames.Length)];
	    string streetName = streetNames[rng.Next(streetNames.Length)];
	    int number = rng.Next(1, 100);
	    string address = $"{townName}, {streetName} {number}";
	    return address;
	}
    }
}
