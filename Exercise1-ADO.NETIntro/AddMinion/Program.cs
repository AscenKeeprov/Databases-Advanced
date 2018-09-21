using System;
using System.Data.SqlClient;
using System.IO;

namespace AddMinion
{
    public class Program
    {
	public static void Main()
	{
	    string[] minionInfo = Console.ReadLine().Split();
	    string minionName = minionInfo[1];
	    int minionAge = int.Parse(minionInfo[2]);
	    string minionTown = minionInfo[3];
	    string[] villainInfo = Console.ReadLine().Split();
	    string villainName = villainInfo[1];
	    using (SqlConnection connection = new SqlConnection(MinionsDB.StartUp.SQLServerExpressConnection))
	    {
		connection.Open();
		int townId = GetTownId(minionTown, connection);
		int minionId = GetMinionId(minionName, minionAge, townId, connection);
		int villainId = GetVillainId(villainName, connection);
		VillainEnslaveMinion(minionId, minionName, villainId, villainName, connection);
		connection.Close();
	    }
	}

	private static int GetTownId(string townName, SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\AddMinion-GetTownId.sql");
	    object townId = null;
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		command.Parameters.AddWithValue("@townName", townName);
		try
		{
		    townId = command.ExecuteScalar();
		    if (townId == null) townId = AddTown(townName, connection);
		}
		catch (SqlException exception)
		{
		    if (exception.Number == 911)
		    {
			InitializeDatabase(connection);
			townId = GetTownId(townName, connection);
		    }
		}
	    }
	    return (int)townId;
	}

	private static int AddTown(string townName, SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\AddMinion-AddTown.sql");
	    int townId = -1;
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		command.Parameters.AddWithValue("@townName", townName);
		townId = (int)command.ExecuteScalar();
		Console.WriteLine($"Town {townName} was added to the database.");
	    }
	    return townId;
	}

	private static int GetMinionId(string minionName, int minionAge, int townId, SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\AddMinion-GetMinionId.sql");
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		command.Parameters.AddWithValue("@minionName", minionName);
		command.Parameters.AddWithValue("@minionAge", minionAge);
		command.Parameters.AddWithValue("@townId", townId);
		return (int)command.ExecuteScalar();
	    }
	}

	private static int GetVillainId(string villainName, SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\AddMinion-GetVillainId.sql");
	    object villainId = null;
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		command.Parameters.AddWithValue("@villainName", villainName);
		villainId = command.ExecuteScalar();
		if (villainId == null) villainId = AddVillain(villainName, connection);
	    }
	    return (int)villainId;
	}

	private static object AddVillain(string villainName, SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\AddMinion-AddVillain.sql");
	    int villainId = -1;
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		command.Parameters.AddWithValue("@villainName", villainName);
		villainId = (int)command.ExecuteScalar();
		Console.WriteLine($"Villain {villainName} was added to the database.");
	    }
	    return villainId;
	}

	private static void VillainEnslaveMinion(int minionId, string minionName,
	    int villainId, string villainName, SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\AddMinion-EnslaveMinion.sql");
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		command.Parameters.AddWithValue("@minionId", minionId);
		command.Parameters.AddWithValue("@villainId", villainId);
		command.ExecuteNonQuery();
		Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
	    }
	}

	private static void InitializeDatabase(SqlConnection connection)
	{
	    MinionsDB.StartUp.Main();
	    Console.Clear();
	}
    }
}
