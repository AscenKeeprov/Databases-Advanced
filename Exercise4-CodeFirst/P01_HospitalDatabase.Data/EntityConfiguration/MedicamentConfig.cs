namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_HospitalDatabase.Data.Models;

    public class MedicamentConfig : IEntityTypeConfiguration<Medicament>
    {
	public void Configure(EntityTypeBuilder<Medicament> builder)
	{
	    builder.ToTable("Medicaments");

	    builder.HasKey(m => m.MedicamentId);

	    builder.Property(m => m.Name)
		.HasMaxLength(50)
		.IsUnicode(true);

	    builder.HasMany(m => m.Prescriptions)
		.WithOne(pr => pr.Medicament)
		.HasForeignKey(pr => pr.MedicamentId);
	}
    }
}
