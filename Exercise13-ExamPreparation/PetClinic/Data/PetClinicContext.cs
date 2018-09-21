namespace PetClinic.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetClinic.Data.EntityConfiguration;
    using PetClinic.Models;

    public class PetClinicContext : DbContext
    {
	public PetClinicContext() { }

	public PetClinicContext(DbContextOptions options) : base(options) { }

	public virtual DbSet<Animal> Animals { get; set; }
	public virtual DbSet<AnimalAid> AnimalAids { get; set; }
	public virtual DbSet<Passport> Passports { get; set; }
	public virtual DbSet<Procedure> Procedures { get; set; }
	public virtual DbSet<ProcedureAnimalAid> ProceduresAnimalAids { get; set; }
	public virtual DbSet<Vet> Vets { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
	    if (!optionsBuilder.IsConfigured)
	    {
		optionsBuilder.UseSqlServer(Configuration.ConnectionString);
	    }
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
	    builder.ApplyConfiguration(new AnimalAidConfiguration());
	    builder.ApplyConfiguration(new AnimalConfiguration());
	    builder.ApplyConfiguration(new PassportConfiguration());
	    builder.ApplyConfiguration(new ProcedureAnimalAidConfiguration());
	    builder.ApplyConfiguration(new ProcedureConfiguration());
	    builder.ApplyConfiguration(new VetConfiguration());
	}
    }
}
