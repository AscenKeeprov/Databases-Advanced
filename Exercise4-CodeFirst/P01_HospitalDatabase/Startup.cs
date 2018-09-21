namespace P01_HospitalDatabase
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data;
    using P01_HospitalDatabase.Data.Models;
    using P01_HospitalDatabase.Initializer;

    public class Startup
    {
	public static void Main()
	{
	    using (var db = new HospitalContext())
	    {
		//DatabaseInitializer.ReseedDatabase(db);
	    }
	}
    }
}
