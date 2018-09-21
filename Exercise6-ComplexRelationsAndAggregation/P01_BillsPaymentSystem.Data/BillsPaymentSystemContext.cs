using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.EntityConfig;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data
{
    public class BillsPaymentSystemContext : DbContext
    {
	public BillsPaymentSystemContext() { }

	public BillsPaymentSystemContext(DbContextOptions options) : base(options) { }

	public virtual DbSet<User> Users { get; set; }
	public virtual DbSet<CreditCard> CreditCards { get; set; }
	public virtual DbSet<BankAccount> BankAccounts { get; set; }
	public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
	    if (!optionsBuilder.IsConfigured)
	    {
		optionsBuilder.UseLazyLoadingProxies(true)
		    .UseSqlServer(DbConfig.ConnectionString);
	    }
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	    modelBuilder.ApplyConfiguration(new UserConfig());
	    modelBuilder.ApplyConfiguration(new CreditCardConfig());
	    modelBuilder.ApplyConfiguration(new BankAccountConfig());
	    modelBuilder.ApplyConfiguration(new PaymentMethodConfig());
	}
    }
}
