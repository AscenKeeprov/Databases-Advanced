namespace P01_HospitalDatabase.Initializer.Generators
{
    using System;

    internal static class DateGenerator
    {
	private static Random rng = new Random();

	internal static DateTime GenerateDate()
	{
	    int year = rng.Next(1998, 2019);
	    int month = rng.Next(1, 13);
	    int day = rng.Next(1, 29);
	    DateTime date = new DateTime(year, month, day);
	    return date;
	}
    }
}
