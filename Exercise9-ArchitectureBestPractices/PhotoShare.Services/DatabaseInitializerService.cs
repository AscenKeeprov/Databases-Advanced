namespace PhotoShare.Services
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Services.Contracts;

    public class DatabaseInitializerService : IDatabaseInitializerService
    {
	private readonly PhotoShareContext context;

	public DatabaseInitializerService(PhotoShareContext context)
	{
	    this.context = context;
	}

	public void InitializeDatabase()
	{
	    context.Database.Migrate();
	}
    }
}
