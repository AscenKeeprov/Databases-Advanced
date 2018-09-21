using System;
using System.Threading;
using DeltaTradeOnlineLTD.Data;
using DeltaTradeOnlineLTD.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeltaTradeOnlineLTD.Services
{
    public class DbInitializer : IDbInitializer
    {
	private DeltaTradeOnlineDbContext dbContext;

	public DbInitializer(DeltaTradeOnlineDbContext dbContext)
	{
	    this.dbContext = dbContext;
	}

	public void InitializeDatabase()
	{
	    Console.Write("Connecting to Delta Trade Online Ltd. database ");
	    for (int second = 1; second <= 3; second++)
	    {
		Thread.Sleep(768);
		Console.Write(".");
	    }
	    dbContext.Database.Migrate();
	    Console.WriteLine($"{Environment.NewLine}Connection established successfully!");
	}
    }
}
