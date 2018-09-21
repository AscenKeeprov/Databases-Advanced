using Microsoft.EntityFrameworkCore;
using TeamBuilder.Data.EntityConfiguration;
using TeamBuilder.Models;

namespace TeamBuilder.Data
{
    public class TeamBuilderDbContext : DbContext
    {
	public TeamBuilderDbContext() { }

	public TeamBuilderDbContext(DbContextOptions options) : base(options) { }

	public virtual DbSet<User> Users { get; set; }
	public virtual DbSet<Team> Teams { get; set; }
	public virtual DbSet<UserTeam> UserTeams { get; set; }
	public virtual DbSet<Invitation> Invitations { get; set; }
	public virtual DbSet<Event> Events { get; set; }
	public virtual DbSet<EventTeam> EventTeams { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
	    if (!optionsBuilder.IsConfigured)
	    {
		optionsBuilder.UseLazyLoadingProxies(true);
		optionsBuilder.UseSqlServer(TeamBuilderDbConfiguration.ConnectionString);
	    }
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	    modelBuilder.ApplyConfiguration(new UserConfiguration());
	    modelBuilder.ApplyConfiguration(new TeamConfiguration());
	    modelBuilder.ApplyConfiguration(new UserTeamConfiguration());
	    modelBuilder.ApplyConfiguration(new InvitationConfiguration());
	    modelBuilder.ApplyConfiguration(new EventConfiguration());
	    modelBuilder.ApplyConfiguration(new EventTeamConfiguration());
	}
    }
}
