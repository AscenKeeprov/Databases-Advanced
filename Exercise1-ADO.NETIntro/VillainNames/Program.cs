using System;
using System.Data.SqlClient;
using System.IO;

namespace VillainNames
{
    public class Program
    {
	public static void Main()
	{
	    using (SqlConnection connection = new SqlConnection(MinionsDB.StartUp.SQLServerExpressConnection))
	    {
		connection.Open();
		GetVillainNames(connection);
		connection.Close();
	    }
	}

	private static void GetVillainNames(SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\VillainNames.sql");
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		try
		{
		    using (SqlDataReader reader = command.ExecuteReader())
		    {
			string col1Header = "Villain Name";
			string col2Header = "Minions Enslaved";
			string topSeparator = new String('─', col1Header.Length + 1) + "┬" + new String('─', col2Header.Length + 1);
			string bottomSeparator = new String('─', col1Header.Length + 1) + "┴" + new String('─', col2Header.Length + 1);
			Console.WriteLine(topSeparator);
			Console.WriteLine($"{col1Header} | {col2Header}");
			Console.WriteLine(bottomSeparator);
			while (reader.Read())
			{
			    Console.WriteLine($"{reader[0]} - {reader[1]}");
			}
		    }
		}
		catch (SqlException exception)
		{
		    if (exception.Number == 911)
		    {
			InitializeDatabase(connection);
			GetVillainNames(connection);
		    }
		}
	    }
	}

	private static void InitializeDatabase(SqlConnection connection)
	{
	    MinionsDB.StartUp.Main();
	    Console.Clear();
	}
    }
}
