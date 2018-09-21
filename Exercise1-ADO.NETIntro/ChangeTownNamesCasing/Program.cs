using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace ChangeTownNamesCasing
{
    public class Program
    {
	public static void Main()
	{
	    string countryName = Console.ReadLine();
	    using (SqlConnection connection = new SqlConnection(MinionsDB.StartUp.SQLServerExpressConnection))
	    {
		connection.Open();
		int countryCode = GetCountryCode(countryName, connection);
		int namesChanged = TownNamesToUpper(countryCode, connection);
		if (namesChanged > 0)
		{
		    Console.WriteLine($"{namesChanged} town names were affected.");
		    PrintTownNames(countryCode, connection);
		}
		else Console.WriteLine("No town names were affected.");
		connection.Close();
	    }
	}

	private static int GetCountryCode(string countryName, SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\ChangeTownNames-GetCountryCode.sql");
	    object countryCode = null;
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		command.Parameters.AddWithValue("@countryName", countryName);
		try
		{
		    countryCode = command.ExecuteScalar();
		    if (countryCode == null) return -1;
		}
		catch (SqlException exception)
		{
		    if (exception.Number == 911)
		    {
			InitializeDatabase(connection);
			countryCode = GetCountryCode(countryName, connection);
		    }
		}
	    }
	    return (int)countryCode;
	}

	private static int TownNamesToUpper(int countryCode, SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\ChangeTownNames-SetToUpper.sql");
	    int namesChanged = 0;
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		command.Parameters.AddWithValue("@countryCode", countryCode);
		namesChanged = command.ExecuteNonQuery();
	    }
	    return namesChanged;
	}

	private static void PrintTownNames(int countryCode, SqlConnection connection)
	{
	    StringBuilder townNames = new StringBuilder();
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\ChangeTownNames-GetChangedNames.sql");
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		command.Parameters.AddWithValue("@countryCode", countryCode);
		using (SqlDataReader reader = command.ExecuteReader())
		{
		    while (reader.Read())
		    {
			townNames.Append($"{reader[0]}, ");
		    }
		}
	    }
	    Console.WriteLine($"[{townNames.ToString().TrimEnd(',', ' ')}]");
	}

	private static void InitializeDatabase(SqlConnection connection)
	{
	    MinionsDB.StartUp.Main();
	    Console.Clear();
	}
    }
}
