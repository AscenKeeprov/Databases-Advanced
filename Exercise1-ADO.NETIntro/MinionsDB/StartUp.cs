using System;
using System.Data.SqlClient;
using System.IO;

namespace MinionsDB
{
    public class StartUp
    {
	public const string SQLServerExpressConnection = @"Server=.\SQLEXPRESS;Integrated Security=true";

	public static void Main()
	{
	    using (SqlConnection connection = new SqlConnection(SQLServerExpressConnection))
	    {
		connection.Open();
		CreateDatabase(connection);
		CreateTables(connection);
		InsertData(connection);
		connection.Close();
	    }
	}

	public static void Main(SqlConnection connection)
	{
	    CreateDatabase(connection);
	    CreateTables(connection);
	    InsertData(connection);
	}

	private static void CreateDatabase(SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\CreateDatabase.sql");
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		try
		{
		    Console.WriteLine("Creating database MinionsDB...");
		    command.ExecuteNonQuery();
		    Console.WriteLine("Database created successfully!");
		}
		catch (SqlException exception)
		{
		    Console.WriteLine(exception.Message);
		}
	    }
	}

	private static void CreateTables(SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\CreateTables.sql");
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		try
		{
		    Console.WriteLine("Creating tables for database MinionsDB...");
		    command.ExecuteNonQuery();
		    Console.WriteLine("Tables created successfully!");
		}
		catch (SqlException exception)
		{
		    if (exception.Number == 911)
		    {
			Console.WriteLine("Database MinionsDB does not exist!");
			CreateDatabase(connection);
			CreateTables(connection);
		    }
		}
	    }
	}

	private static void InsertData(SqlConnection connection)
	{
	    string sql = File.ReadAllText(@".\..\..\..\..\MinionsDB\Scripts\InsertData.sql");
	    using (SqlCommand command = new SqlCommand(sql, connection))
	    {
		try
		{
		    Console.WriteLine("Loading information into database tables...");
		    command.ExecuteNonQuery();
		    Console.WriteLine("Data load complete!");
		}
		catch (SqlException exception)
		{
		    if (exception.Number == 911)
		    {
			Console.WriteLine("Database MinionsDB does not exist.");
			CreateDatabase(connection);
			CreateTables(connection);
			InsertData(connection);
		    }
		    else if (exception.Number == 208)
		    {
			Console.WriteLine("Database Minions DB does not contain any tables!");
			CreateTables(connection);
			InsertData(connection);
		    }
		}
	    }
	}
    }
}
