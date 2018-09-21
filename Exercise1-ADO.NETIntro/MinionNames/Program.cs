using System;
using System.Data.SqlClient;
using System.IO;

namespace MinionNames
{
    public class Program
    {
	public static void Main()
	{
	    int villainId = int.Parse(Console.ReadLine());
	    using (SqlConnection connection = new SqlConnection(MinionsDB.StartUp.SQLServerExpressConnection))
	    {
		connection.Open();
		string villainName = GetMasterName(villainId, connection);
		if (villainName == null)
		    Console.WriteLine($"No villain with ID {villainId} exists in the database.");
		else
		{
		    Console.WriteLine($"Villain: {villainName}");
		    GetMinionNames(villainId, connection);
		}
		connection.Close();
	    }
	}

	private static string GetMasterName(int villainId, SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\MinionNames-Master.sql");
	    string villainName = String.Empty;
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		command.Parameters.AddWithValue("@villainId", villainId);
		try
		{
		    villainName = (string)command.ExecuteScalar();
		}
		catch (SqlException exception)
		{
		    if (exception.Number == 911)
		    {
			InitializeDatabase(connection);
			villainName = GetMasterName(villainId, connection);
		    }
		}
	    }
	    return villainName;
	}

	private static void GetMinionNames(int villainId, SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\MinionNames-Slaves.sql");
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		command.Parameters.AddWithValue("@villainId", villainId);
		using (SqlDataReader reader = command.ExecuteReader())
		{
		    if (reader.HasRows)
		    {
			int row = 1;
			while (reader.Read())
			{
			    Console.WriteLine($"{row++}. {reader[0]} {reader[1]}");
			}
		    }
		    else Console.WriteLine("(no minions)");
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
