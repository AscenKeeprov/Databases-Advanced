namespace MiniORM
{
    using System;

    /// <summary>
    /// Used for wrapping a database connection with a using statement and
    /// automatically closing it when the using statement ends
    /// </summary>
    internal class ConnectionManager : IDisposable
    {
	private readonly DbConnection connection;

	public ConnectionManager(DbConnection connection)
	{
	    this.connection = connection;
	    this.connection.Open();
	}

	public void Dispose()
	{
	    connection.Close();
	}
    }
}
