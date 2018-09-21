namespace P03_SalesDatabase
{
    using System;
    using System.Linq;
    using P03_SalesDatabase.Data;

    public class Startup
    {
        public static void Main()
        {
            using (var db = new SalesContext())
	    {
		var products = db.Products.ToArray();
		foreach (var product in products)
		{
		    Console.WriteLine(product.ToString());
		}
	    }
        }
    }
}
