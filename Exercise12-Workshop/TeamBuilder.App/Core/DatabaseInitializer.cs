using Microsoft.EntityFrameworkCore;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
	private readonly TeamBuilderDbContext dbContext;

	public DatabaseInitializer(TeamBuilderDbContext dbContext)
	{
	    this.dbContext = dbContext;
	}

	public void InitializeDatabase()
	{
	    dbContext.Database.Migrate();
	}
    }
}
