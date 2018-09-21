namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_HospitalDatabase.Data.Models;

    public class PatientMedicamentConfig : IEntityTypeConfiguration<PatientMedicament>
    {
	public void Configure(EntityTypeBuilder<PatientMedicament> builder)
	{
	    builder.ToTable("PatientsMedicaments");

	    builder.HasKey(pm => new { pm.PatientId, pm.MedicamentId });

	    builder.HasOne(pm => pm.Patient)
		.WithMany(p => p.Prescriptions)
		.HasForeignKey(pm => pm.PatientId);
	}
    }
}
