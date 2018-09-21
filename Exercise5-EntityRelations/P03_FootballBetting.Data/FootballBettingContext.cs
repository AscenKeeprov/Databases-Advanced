using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.EntityConfiguration;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
	public FootballBettingContext() { }

	public FootballBettingContext(DbContextOptions options) : base(options) { }

	public virtual DbSet<Team> Teams { get; set; }
	public virtual DbSet<Color> Colors { get; set; }
	public virtual DbSet<Town> Towns { get; set; }
	public virtual DbSet<Country> Countries { get; set; }
	public virtual DbSet<Player> Players { get; set; }
	public virtual DbSet<Position> Positions { get; set; }
	public virtual DbSet<PlayerStatistic> PlayerStatistics { get; set; }
	public virtual DbSet<Game> Games { get; set; }
	public virtual DbSet<Bet> Bets { get; set; }
	public virtual DbSet<User> Users { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
	    if (!optionsBuilder.IsConfigured)
		optionsBuilder.UseSqlServer(ContextConfiguration.ConnectionString);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	    modelBuilder.ApplyConfiguration(new BetConfig());
	    modelBuilder.ApplyConfiguration(new ColorConfig());
	    modelBuilder.ApplyConfiguration(new CountryConfig());
	    modelBuilder.ApplyConfiguration(new GameConfig());
	    modelBuilder.ApplyConfiguration(new PlayerConfig());
	    modelBuilder.ApplyConfiguration(new PlayerStatisticConfig());
	    modelBuilder.ApplyConfiguration(new PositionConfig());
	    modelBuilder.ApplyConfiguration(new TeamConfig());
	    modelBuilder.ApplyConfiguration(new TownConfig());
	    modelBuilder.ApplyConfiguration(new UserConfig());
	}
    }
}
