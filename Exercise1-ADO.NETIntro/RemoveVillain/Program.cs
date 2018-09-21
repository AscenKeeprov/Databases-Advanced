using System;
using System.Data.SqlClient;
using System.IO;

namespace RemoveVillain
{
    public class Program
    {
	public static void Main()
	{
	    int villainId = int.Parse(Console.ReadLine());
	    using (SqlConnection connection = new SqlConnection(MinionsDB.StartUp.SQLServerExpressConnection))
	    {
		connection.Open();
		string villainName = GetVillainName(villainId, connection);
		if (String.IsNullOrEmpty(villainName))
		    Console.WriteLine("No such villain was found.");
		else
		{
		    using (SqlTransaction transaction = connection.BeginTransaction())
		    {
			try
			{
			    int minionsReleased = ReleaseVillainMinions(villainId, connection, transaction);
			    BanishVillain(villainId, connection, transaction);
			    transaction.Commit();
			    Console.WriteLine($"{villainName} was deleted.");
			    Console.WriteLine($"{minionsReleased} minions were released.");
			}
			catch (SqlException)
			{
			    transaction.Rollback();
			}
		    }
		}
		connection.Close();
	    }

	}

	private static string GetVillainName(int villainId, SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\RemoveVillain-GetVillainName.sql");
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
			villainName = GetVillainName(villainId, connection);
		    }
		}
	    }
	    return villainName;
	}

	private static int ReleaseVillainMinions(int villainId, SqlConnection connection, SqlTransaction transaction)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\RemoveVillain-ReleaseMinions.sql");
	    int minionsReleased = 0;
	    using (SqlCommand command = new SqlCommand(sql, connection, transaction))
	    {
		command.Parameters.AddWithValue("@villainId", villainId);
		minionsReleased = command.ExecuteNonQuery();
		transaction.Save("MinionsReleased");
	    }
	    return minionsReleased;
	}

	private static void BanishVillain(int villainId, SqlConnection connection, SqlTransaction transaction)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\RemoveVillain-BanishVillain.sql");
	    using (SqlCommand command = new SqlCommand(sql, connection, transaction))
	    {
		command.Parameters.AddWithValue("@villainId", villainId);
		command.ExecuteNonQuery();
		transaction.Save("VillainBanished");
	    }
	}

	private static void InitializeDatabase(SqlConnection connection)
	{
	    MinionsDB.StartUp.Main();
	    Console.Clear();
	}
    }
}
