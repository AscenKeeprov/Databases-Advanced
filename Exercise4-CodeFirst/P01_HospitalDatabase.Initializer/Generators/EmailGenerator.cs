namespace P01_HospitalDatabase.Initializer.Generators
{
    using System;

    internal static class EmailGenerator
    {
	private static Random rng = new Random();

	private static string[] domains = new string[]
	{
	    "mail.bg",
	    "abv.bg",
	    "gmail.com",
	    "hotmail.com",
	    "softuni.bg",
	    "students.softuni.bg"
	};

	internal static string GenerateEmail(string name)
	{
	    string domain = domains[rng.Next(domains.Length)];
	    int number = rng.Next(1, 2000);
	    string email = $"{name.ToLower()}{number}@{domain}";
	    return email;
	}
    }
}
